pipeline {
    agent none
    environment {
        BRANCH_LOWER=BRANCH_NAME.toLowerCase()
        VANITY_URL="${BRANCH_LOWER}.pharmanetenrolment.gov.bc.ca"
        SCHEMA="https"
        PORT="8443"
        FRONTEND_ARGS="-p REDIRECT_URL=${SCHEMA}://${VANITY_URL} -p VANITY_URL=${VANITY_URL}"
        API_ARGS="-p ASPNETCORE_ENVIRONMENT=Development -p VANITY_URL=${VANITY_URL}"
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
                checkout scm
                echo "Building ..."
                sh "./player.sh build database dev"
                sh "./player.sh build api dev ${API_ARGS}"
                sh "./player.sh build frontend dev ${FRONTEND_ARGS}"
            }
        }
        stage('Deploy Branch') {
            options {
                timeout(time: 10, unit: 'MINUTES')   // timeout on this stage
            }
            when { expression { ( GIT_BRANCH != 'master' ) } }
            agent { label 'master' }
            steps {
                echo "Deploy to dev..."
                sh "./player.sh deploy database dev"
                sh "./player.sh deploy api dev ${API_ARGS}"
                sh "./player.sh deploy frontend dev ${FRONTEND_ARGS}"
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
