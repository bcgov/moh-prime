pipeline {
    agent none
    options {
        disableResume()
    }
    stages {
        stage('Verify-Files') {
            agent { label 'master' }
            steps {
                echo "Aborting all running jobs ..."
                script {
                    // Grab any files under the pipeline directory
                    // Verify they match the trusted version
                    files = findFiles(glob: 'pipeline/**')
                    for (def file : files) {
                        readTrusted file.path
                    }
                }
            }
        }
        stage('Build') {
            agent { label 'master' }
            steps {
                echo "Building ..."
                sh 'unset JAVA_OPTS; pipeline/gradlew --no-build-cache --console=plain --no-daemon -b pipeline/build.gradle cd-build -Pargs.--config=pipeline/config-build.groovy -Pargs.--pr=${CHANGE_ID}'
            }
        }
        stage('Deploy (DEV)') {
            agent { label 'master' }
            steps {
                echo "Deploy (DEV) ..."
                sh 'unset JAVA_OPTS; pipeline/gradlew --no-build-cache --console=plain --no-daemon -b pipeline/build.gradle cd-deploy -Pargs.--config=pipeline/config-dev.groovy -Pargs.--pr=${CHANGE_ID} -Pargs.--env=dev'
            }
        }
        stage('Unit Tests and SonarQube Reporting (DEV)') {
            agent { label 'master' }
            steps {
                echo "Running unit tests and reporting them to SonarQube ..."
                sh 'unset JAVA_OPTS; pipeline/gradlew --no-build-cache --console=plain --no-daemon -b pipeline/build.gradle cd-unit-test -Pargs.--config=pipeline/config-dev.groovy -Pargs.--pr=${CHANGE_ID} -Pargs.--env=dev -Pargs.--branch=${CHANGE_BRANCH}'
            }
        }
        stage('Functional Test (DEV)') {
            agent { label 'master' }
            steps {
                echo "Functional Test (DEV) ..."
                sh 'unset JAVA_OPTS; pipeline/gradlew --no-build-cache --console=plain --no-daemon -b pipeline/build.gradle cd-functional-test -Pargs.--config=pipeline/config-dev.groovy -Pargs.--pr=${CHANGE_ID} -Pargs.--env=dev'
            }
        }
        stage ('ZAP (DEV)'){
            agent { label 'master' }
            steps {
                echo "ZAP (DEV)"
                sh 'unset JAVA_OPTS; pipeline/gradlew --no-build-cache --console=plain --no-daemon -b pipeline/build.gradle cd-zap -Pargs.--config=pipeline/config-dev.groovy -Pargs.--pr=${CHANGE_ID} -Pargs.--env=dev'
            }
        }
        stage('Deploy (TEST)') {
            agent { label 'master' }
            when {
              environment name: 'CHANGE_TARGET', value: 'master'
            }
            steps {
                echo "Deploy (TEST)"
                sh 'unset JAVA_OPTS; pipeline/gradlew --no-build-cache --console=plain --no-daemon -b pipeline/build.gradle cd-deploy -Pargs.--config=pipeline/config-test.groovy -Pargs.--pr=${CHANGE_ID} -Pargs.--env=test'
            }
        }
        stage('Deploy (PROD)') {
            agent { label 'master' }
            when {
              environment name: 'CHANGE_TARGET', value: 'master'
            }
            steps {
                script {
                    def IS_APPROVED = input(message: "Deploy to PROD?", ok: "yes", parameters: [string(name: 'IS_APPROVED', defaultValue: 'yes', description: 'Deploy to PROD?')])
                    if (IS_APPROVED != 'yes') {
                        currentBuild.result = "ABORTED"
                        error "User cancelled"
                    }
                    echo "Deploy (PROD)"
                    sh 'unset JAVA_OPTS; pipeline/gradlew --no-build-cache --console=plain --no-daemon -b pipeline/build.gradle cd-deploy -Pargs.--config=pipeline/config-prod.groovy -Pargs.--pr=${CHANGE_ID} -Pargs.--env=prod'
                }
            }
        }
        stage('Merge to master') {
            agent { label 'master' }
            when {
              environment name: 'CHANGE_TARGET', value: 'master'
            }
            steps {
                script {
                    def IS_APPROVED = input(message: "Merge to master?", ok: "yes", parameters: [string(name: 'IS_APPROVED', defaultValue: 'yes', description: 'Merge to master?')])
                    if (IS_APPROVED != 'yes') {
                        currentBuild.result = "ABORTED"
                        error "User cancelled"
                    }
                    echo "Squashing commits and merging to master"
                }
                withCredentials([usernamePassword(credentialsId: 'github-account', passwordVariable: 'GIT_PASSWORD', usernameVariable: 'GIT_USERNAME')]) {
                    sh """
                        # Update master with latest changes from develop
                        git checkout master
                        git fetch
                        git merge --squash origin/develop
                        git commit -m "Merge branch develop into master"
                        git push https://${GIT_USERNAME}:${GIT_PASSWORD}@github.com/bcgov/mds.git

                        # Update the HEAD on develop to be the same as master
                        git checkout develop
                        git fetch
                        git merge -s ours -m "Updating develop with master" origin/master
                        git push https://${GIT_USERNAME}:${GIT_PASSWORD}@github.com/bcgov/mds.git
                    """
                }
            }
        }
        stage('Acceptance') {
            agent { label 'master' }
            input {
                message "Should we continue with cleanup?"
                ok "Yes!"
            }
            steps {
                echo "Acceptance ..."
                sh 'unset JAVA_OPTS; pipeline/gradlew --no-build-cache --console=plain --no-daemon -b pipeline/build.gradle cd-clean -Pargs.--config=pipeline/config-dev.groovy -Pargs.--pr=${CHANGE_ID}'
            }
        }
    }
    agent {
        node {label 'python'}
    }
    environment {
        APPLICATION_NAME = 'python-nginx'
        GIT_REPO="http://github.com/ruddra/openshift-python-nginx.git"
        GIT_BRANCH="master"
        STAGE_TAG = "promoteToQA"
        DEV_PROJECT = "dqszvc-tools.pathfinder.gov.bc.ca"
        STAGE_PROJECT = "stage"
        TEMPLATE_NAME = "python-nginx"
        ARTIFACT_FOLDER = "target"
        PORT = 8081;
    }
    stage('Get Latest Code') {
        steps {
            git branch: "${GIT_BRANCH}", url: "${GIT_REPO}" // declared in environment
        }
    }
    stage("Install Dependencies") {
        steps {
            sh """
            pip install virtualenv
            virtualenv --no-site-packages .
            source bin/activate
            pip install -r app/requirements.pip
            deactivate
            """
        }
    }
    stage('Run Tests') {
        steps {
            sh '''
            source bin/activate
            nosetests app --with-xunit
            deactivate
            '''
            junit "nosetests.xml"
        }
    }
    stage('Store Artifact'){
        steps{
            script{
                def safeBuildName  = "${APPLICATION_NAME}_${BUILD_NUMBER}",
                    artifactFolder = "${ARTIFACT_FOLDER}",
                    fullFileName   = "${safeBuildName}.tar.gz",
                    applicationZip = "${artifactFolder}/${fullFileName}"
                    applicationDir = ["app",
                                    "config",
                                    "Dockerfile",
                                    ].join(" ");
                def needTargetPath = !fileExists("${artifactFolder}")
                if (needTargetPath) {
                    sh "mkdir ${artifactFolder}"
                }
                sh "tar -czvf ${applicationZip} ${applicationDir}"
                archiveArtifacts artifacts: "${applicationZip}", excludes: null,               onlyIfSuccessful: true
            }
        }
    }
    stage('Create Image Builder') {
        when {
            expression {
                openshift.withCluster() {
                openshift.withProject(DEV_PROJECT) {
                    return !openshift.selector("bc", "${TEMPLATE_NAME}").exists();
                    }
                }
            }
        }
        steps {
            script {
                openshift.withCluster() {
                    openshift.withProject(DEV_PROJECT) {
                        openshift.newBuild("--name=${TEMPLATE_NAME}", "--docker-image=docker.io/nginx:mainline-alpine", "--binary=true")
                    }
                }
            }
        }
    }
    stage('Build Image') {
        steps {
            script {
                openshift.withCluster() {
                openshift.withProject(env.DEV_PROJECT) {
                    openshift.selector("bc", "$TEMPLATE_NAME").startBuild("--from-archive=${ARTIFACT_FOLDER}/${APPLICATION_NAME}_${BUILD_NUMBER}.tar.gz", "--wait=true")
                    }
                }
            }
        }
    }
    stage('Deploy to DEV') {
        when {
            expression {
                openshift.withCluster() {
                openshift.withProject(env.DEV_PROJECT) {
                    return !openshift.selector('dc', "${TEMPLATE_NAME}").exists()
                    }
                }
            }
        }
        steps {
            script {
                openshift.withCluster() {
                    openshift.withProject(env.DEV_PROJECT) {
                        def app = openshift.newApp("${TEMPLATE_NAME}:latest")
                        app.narrow("svc").expose("--port=${PORT}");
                        def dc = openshift.selector("dc", "${TEMPLATE_NAME}")
                        while (dc.object().spec.replicas != dc.object().status.availableReplicas) {
                            sleep 10
                        }
                    }
                }
            }
        }
    }
    stage('Promote to STAGE?') {
        steps {
            timeout(time:15, unit:'MINUTES') {
                input message: "Promote to STAGE?", ok: "Promote"
            }
            script {
                openshift.withCluster() {
                openshift.tag("${DEV_PROJECT}/${TEMPLATE_NAME}:latest", "${STAGE_PROJECT}/${TEMPLATE_NAME}:${STAGE_TAG}")
                }
            }
        }
    }
    stage('Rollout to STAGE') {
    steps {
        script {
                    openshift.withCluster() {
                    openshift.withProject(STAGE_PROJECT) {
                        if (openshift.selector('dc', '${TEMPLATE_NAME}').exists()) {
                            openshift.selector('dc', '${TEMPLATE_NAME}').delete()
                            openshift.selector('svc', '${TEMPLATE_NAME}').delete()
                            openshift.selector('route', '${TEMPLATE_NAME}').delete()
                        }
                    openshift.newApp("${TEMPLATE_NAME}:${STAGE_TAG}").narrow("svc").expose("--port=${PORT}")
                    }
                }
            }
        }
    }
    stage('Scale in STAGE') {
    steps {
        script {
            openshiftScale(namespace: "${STAGE_PROJECT}", deploymentConfig: "${TEMPLATE_NAME}", replicaCount: '3')
            }
        }
    }
}