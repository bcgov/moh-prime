pipeline {
    agent none
    environment {
        BRANCH_LOWER=BRANCH_NAME.toLowerCase()
        VANITY_URL="angular-frontend-${BRANCH_LOWER}-dqszvc-dev.pathfinder.gov.bc.ca"
        HTTP_SCHEMA="http"
        HTTP_PORT="8080"
        FRONTEND_ARGS="-p HTTP_PORT=${HTTP_PORT} -p HTTP_SCHEMA=${HTTP_SCHEMA} TERMINATION_TYPE='Edge' -p REDIRECT_URL=${HTTP_SCHEMA}://${VANITY_URL} -p VANITY_URL=${VANITY_URL}"
        API_ARGS="-p ASPNETCORE_ENVIRONMENT=Development -p HTTP_PORT=${HTTP_PORT} -p HTTP_SCHEMA=${HTTP_SCHEMA} -p VANITY_URL=${VANITY_URL}"
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
