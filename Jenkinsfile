pipeline {
    agent none
    environment {
        BRANCH_LOWER=BRANCH_NAME.toLowerCase()
        VANITY_URL="${BRANCH_LOWER}.pharmanetenrolment.gov.bc.ca"
        SCHEMA="https"
        PORT="8443"
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
                    if (env.BRANCH_NAME == 'develop') {
                        SUFFIX=''
                    } else {
                        SUFFIX="-${BRANCH_LOWER}"
                    }
                    checkout scm
                    echo "Building ..."
                    sh "./player.sh build database dev -p SUFFIX='${SUFFIX}'"
                    sh "./player.sh build api dev ${API_ARGS}"
                    sh "./player.sh build frontend dev ${FRONTEND_ARGS}"
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
                    if (env.BRANCH_NAME == 'develop') {
                        SUFFIX=''
                    } else {
                        SUFFIX="-${BRANCH_LOWER}"
                    }
                    echo "Deploy to dev..."
                    sh "./player.sh deploy database dev -p SUFFIX='${SUFFIX}'"
                    sh "./player.sh deploy api dev ${API_ARGS}"
                    sh "./player.sh deploy frontend dev ${FRONTEND_ARGS}"
                }
            }
        }
        stage('SchemaSpy Database Investigation') {
            when { expression { ( GIT_BRANCH == 'develop' ) } }
            agent { label 'master' }
            steps {
                sh "./player.sh toolbelt schemaspy dev"
            }
        }
    }
}
