pipeline {
    agent none
    options {
        disableResume()
    }
environment {
    APPLICATION_NAME = 'moh-prime'
    GIT_REPO="http://github.com/bcgov/moh-prime.git"
    GIT_BRANCH="develop"
    STAGE_TAG = "promoteToQA"
    DEV_PROJECT = "dev"
    STAGE_PROJECT = "stage"
    TEMPLATE_NAME = "moh-prime-postgres"
    ARTIFACT_FOLDER = "target"
    PORT = 5432;
    }
    stages {
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
                archiveArtifacts artifacts: "${applicationZip}", excludes: null, onlyIfSuccessful: true
            }
        }
    }