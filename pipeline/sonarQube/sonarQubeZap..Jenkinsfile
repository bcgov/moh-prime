// ================================================================================================
// SonarQube Scanner Settings
// ------------------------------------------------------------------------------------------------

// The name of the SonarQube route.  Used to dynamically get the URL for SonarQube.
def SONAR_ROUTE_NAME = 'sonarqube'

// The namespace in which the SonarQube route resides.  Used to dynamically get the URL for SonarQube.
// Leave blank if the pipeline is running in same namespace as the route.
def SONAR_ROUTE_NAMESPACE = 'dqszvc-tools'

// The name of your SonarQube project
def SONAR_PROJECT_NAME = 'Optimize Prime'

// The project key of your SonarQube project
def SONAR_PROJECT_KEY = 'OptimizePrime'

// The base directory of your project.
// This is relative to the location of the `sonar-runner` directory within your project.
// More accurately this is relative to the Gradle build script(s) that manage the SonarQube Scanning
def SONAR_PROJECT_BASE_DIR = '../'

// The source code directory you want to scan.
// This is relative to the project base directory.
def SONAR_SOURCES = './zap/wrk'
// ================================================================================================

// ================================================================================================
// ZAP Scanner Settings
// ------------------------------------------------------------------------------------------------

// The name of the target route.  This will be used to dynamically get the URL.
def TARGET_ROUTE = 'angular-on-nginx'

// The namespace in which the target route can be found.  This will be used to dynamically get the URL.
def TARGET_PROJECT_NAMESPACE = 'devex-von-dev'

// The path to the API.
def API_PATH='/api'

// The API format; either openapi or soap
def API_FORMAT = 'openapi'

// The name  of the ZAP report
def ZAP_REPORT_NAME = "zap-report.xml"

// The location of the ZAP reports
def ZAP_REPORT_PATH = "/zap/wrk/${ZAP_REPORT_NAME}"

// The name of the "stash" containing the ZAP report
def ZAP_REPORT_STASH = "zap-report"
// ================================================================================================

@NonCPS
String getUrlForRoute(String routeName, String projectNameSpace = '') {

  def nameSpaceFlag = ''
  if(projectNameSpace?.trim()) {
    nameSpaceFlag = "-n ${projectNameSpace}"
  }
  
  def url = sh (
    script: "oc get routes ${nameSpaceFlag} -o wide --no-headers | awk \'/${routeName}/{ print match(\$0,/edge/) ?  \"https://\"\$2 : \"http://\"\$2 }\'",
    returnStdout: true
  ).trim()

  return url
}

@NonCPS
String getSonarQubePwd() {

  sonarQubePwd = sh (
    script: 'oc env dc/sonarqube --list | awk  -F  "=" \'/SONARQUBE_ADMINPW/{print $2}\'',
    returnStdout: true
  ).trim()

  return sonarQubePwd
}

// The jenkins-slave-zap image has been purpose built for supporting ZAP scanning.
podTemplate(
  label: 'owasp-zap', 
  name: 'owasp-zap', 
  serviceAccount: 'jenkins', 
  cloud: 'openshift', 
  containers: [
    containerTemplate(
      name: 'jnlp',
      image: '172.50.0.2:5000/openshift/jenkins-slave-zap',
      resourceRequestCpu: '500m',
      resourceLimitCpu: '1000m',
      resourceRequestMemory: '3Gi',
      resourceLimitMemory: '4Gi',
      workingDir: '/home/jenkins',
      command: '',
      args: '${computer.jnlpmac} ${computer.name}'
    )
  ]
){
  node('owasp-zap') {
    stage('ZAP Security Scan') {

      // Dynamicaly determine the target URL for the ZAP scan ...
      def TARGET_URL = getUrlForRoute(TARGET_ROUTE, TARGET_PROJECT_NAMESPACE).trim()
      def API_TARGET_URL="${TARGET_URL}${API_PATH}/?format=${API_FORMAT}"

      echo "Target URL: ${TARGET_URL}"
      echo "API Target URL: ${API_TARGET_URL}"

      dir('zap') {

        // The ZAP scripts are installed on the root of the jenkins-slave-zap image.
        // When running ZAP from there the reports will be created in /zap/wrk/ by default.
        // ZAP has problems with creating the reports directly in the Jenkins
        // working directory, so they have to be copied over after the fact.
        def retVal = sh (
          returnStatus: true,
          script: "/zap/zap-baseline.py -x ${ZAP_REPORT_NAME} -t ${TARGET_URL}"
          // Other scanner options ...
          // zap-api-scan errors out
          // script: "/zap/zap-api-scan.py -x ${ZAP_REPORT_NAME} -t ${API_TARGET_URL} -f ${API_FORMAT}"
          // script: "/zap/zap-full-scan.py -x ${ZAP_REPORT_NAME} -t ${TARGET_URL}"
        )
        echo "Return value is: ${retVal}"

        // Copy the ZAP report into the Jenkins working directory so the Jenkins tools can access it.
        sh (
          returnStdout: true,
          script: "mkdir -p ./wrk/ && cp ${ZAP_REPORT_PATH} ./wrk/"
        )
      }

      // Stash the ZAP report for publishing in a different stage (which will run on a different pod).
      echo "Stash the report for the publishing stage ..."
      stash name: "${ZAP_REPORT_STASH}", includes: "zap/wrk/*.xml"
    }
  }
}

// The jenkins-python3nodejs template has been purpose built for supporting SonarQube scanning.
podTemplate(
  label: 'jenkins-python3nodejs',
  name: 'jenkins-python3nodejs',
  serviceAccount: 'jenkins',
  cloud: 'openshift',
  containers: [
    containerTemplate(
      name: 'jnlp',
      image: '172.50.0.2:5000/openshift/jenkins-slave-python3nodejs',
      resourceRequestCpu: '1000m',
      resourceLimitCpu: '2000m',
      resourceRequestMemory: '2Gi',
      resourceLimitMemory: '4Gi',
      workingDir: '/tmp',
      command: '',
      args: '${computer.jnlpmac} ${computer.name}'
    )
  ]
){
  node('jenkins-python3nodejs') {

    stage('Publish ZAP Report to SonarQube') {

      // Do a sparse checkout of the sonar-runner folder since it is the only
      // part of the project we need to publish the ZAP report to SonarQube.
      // We're not scanning our source code here ...
      //
      // For this to work the Jenkins Administrator may have to approve the following methods;
      // - method hudson.plugins.git.GitSCM getBranches
      // - method hudson.plugins.git.GitSCM getUserRemoteConfigs
      // - method hudson.plugins.git.GitSCMBackwardCompatibility getExtensions
      // - staticMethod org.codehaus.groovy.runtime.DefaultGroovyMethods plus java.util.Collection java.lang.Object
      echo "Checking out the sonar-runner folder ..."
      checkout([
          $class: 'GitSCM',
          branches: scm.branches,
          extensions: scm.extensions + [
            [$class: 'SparseCheckoutPaths',  sparseCheckoutPaths:[[path:'sonar-runner/']]]
          ],
          userRemoteConfigs: scm.userRemoteConfigs
      ])

      echo "Preparing the report for the publishing ..."
      unstash name: "${ZAP_REPORT_STASH}"

      SONARQUBE_URL = getUrlForRoute(SONAR_ROUTE_NAME).trim()
      SONARQUBE_PWD = getSonarQubePwd().trim()
      echo "URL: ${SONARQUBE_URL}"
      echo "PWD: ${SONARQUBE_PWD}"

      echo "Publishing the report ..."
      // The `sonar-runner` MUST exist in your project and contain a Gradle environment consisting of:
      // - Gradle wrapper script(s)
      // - A simple `build.gradle` file that includes the SonarQube plug-in.
      //
      // An example can be found here:
      // - https://github.com/BCDevOps/sonarqube
      dir('sonar-runner') {
        // ======================================================================================================
        // Set your SonarQube scanner properties at this level, not at the Gradle Build level.
        // The only thing that should be defined at the Gradle Build level is a minimal set of generic defaults.
        //
        // For more information on available properties visit:
        // - https://docs.sonarqube.org/display/SCAN/Analyzing+with+SonarQube+Scanner+for+Gradle
        // ======================================================================================================
        sh (
          // 'sonar.zaproxy.reportPath' must be set to the absolute path of the xml formatted ZAP report.
          // Exclude the report from being scanned as an xml file.  We only care about the results of the ZAP scan.
          returnStdout: true,
          script: "./gradlew sonarqube --stacktrace --info \
            -Dsonar.verbose=true \
            -Dsonar.host.url=${SONARQUBE_URL} \
            -Dsonar.projectName=${SONAR_PROJECT_NAME} \
            -Dsonar.projectKey=${SONAR_PROJECT_KEY} \
            -Dsonar.projectBaseDir=${SONAR_PROJECT_BASE_DIR} \
            -Dsonar.sources=${SONAR_SOURCES} \
            -Dsonar.zaproxy.reportPath=${WORKSPACE}${ZAP_REPORT_PATH} \
            -Dsonar.exclusions=**/*.xml"
        )
      }
    }
  }
}