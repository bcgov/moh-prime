pipeline {
    agent none
    environment {
        BRANCH_LOWER=BRANCH_NAME.toLowerCase()
        VANITY_URL="${BRANCH_LOWER}.pharmanetenrolment.gov.bc.ca"
        SCHEMA="https"
        PORT="8443"
        SUFFIX="-${BRANCH_LOWER}"
        FRONTEND_ARGS="-p REDIRECT_URL=${SCHEMA}://${VANITY_URL} -p VANITY_URL=${VANITY_URL}"
        API_ARGS="-p ASPNETCORE_ENVIRONMENT=Release -p VANITY_URL=${VANITY_URL}"
    }
    options {
        disableResume()

    }
    stages {
        stage('Build Branch') {
            options {
                timeout(time: 90, unit: 'MINUTES')   // timeout on this stage
            }
            when { expression { ( GIT_BRANCH != 'master' ) } }
            agent { label 'master' }
            steps {
                script {
                    checkout scm
                    echo "Building ..."
                    sh "./player.sh build postgres dev -p SUFFIX=${SUFFIX}"
                    sh "./player.sh build api dev ${API_ARGS} -p SUFFIX=${SUFFIX}"
                    sh "./player.sh build frontend dev ${FRONTEND_ARGS} -p SUFFIX=${SUFFIX}"
                }
            }
        }
        stage('Deploy Branch') {
            options {
                timeout(time: 10, unit: 'MINUTES')   // timeout on this stage
            }
            when { expression { ( GIT_BRANCH != 'master' ) } }
            agent { label 'master' }
            steps {
                script {
                    checkout scm
                    echo "Deploy to dev..."
                    sh "printenv"
                    sh "./player.sh deploy postgres dev -p SUFFIX=${SUFFIX} -p VOLUME_CAPACITY=256Mi"
                    sh "./player.sh deploy mongo dev -p SUFFIX=${SUFFIX} -p VOLUME_CAPACITY=256Mi"
                    sh "./player.sh deploy api dev ${API_ARGS} -p SUFFIX=${SUFFIX}"
                    sh "./player.sh deploy frontend dev ${FRONTEND_ARGS} -p SUFFIX=${SUFFIX}"
                }
            }
        }
        stage('Quality Check') {
            options {
                timeout(time: 10, unit: 'MINUTES')   // timeout on this stage
            }
            when { expression { ( BRANCH_NAME == 'develop' ) } }
            parallel {
                stage('SonarQube Code Check') {
                    agent { label 'code-tests' }
                    steps {
                        sh "./player.sh scan"
                    }      
                }
                stage('ZAP') {
                    agent { label 'code-tests' }
                    steps {
                        checkout scm
                        echo "Scanning..."
                        sh "./player.sh zap frontend"
                    }
                }
                stage('SchemaSpy Database Investigation') {
                    agent { label 'master' }
                    steps {
                        checkout scm
                        sh "./player.sh toolbelt schemaspy dev"
                    }
                }
            }
        }
    }
}
