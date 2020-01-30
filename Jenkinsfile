pipeline {
    agent none
    environment {
        BRANCH_LOWER=BRANCH_NAME.toLowerCase()
        VANITY_URL="${BRANCH_LOWER}.pharmanetenrolment.gov.bc.ca"
        FRONTEND_ARGS="-p HTTP_PORT=8080 -p HTTP_SCHEMA=http TERMINATION_TYPE='Edge' -p REDIRECT_URL=http://${VANITY_URL} -p VANITY_URL=${VANITY_URL}"
        API_ARGS="-p ASPNETCORE_ENVIRONMENT=Development -p HTTP_PORT=8080 -p HTTP_SCHEMA=http -p VANITY_URL=${VANITY_URL}"
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
