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
                    checkout scm
                    if (env.BRANCH_NAME == 'develop') {
                    FRONTEND_ARGS="-p REDIRECT_URL=${SCHEMA}://${VANITY_URL} -p VANITY_URL=${VANITY_URL}"
                    API_ARGS="-p ASPNETCORE_ENVIRONMENT=Release -p VANITY_URL=${VANITY_URL}"
                    echo "Building ..."
                    sh "./player.sh build database dev"
                    sh "./player.sh build api dev ${API_ARGS}"
                    sh "./player.sh build frontend dev ${FRONTEND_ARGS}"
                    } else {
                    FRONTEND_ARGS="-p REDIRECT_URL=${SCHEMA}://${VANITY_URL} -p VANITY_URL=${VANITY_URL} -p SUFFIX='-${BRANCH_LOWER}'"
                    API_ARGS="-p ASPNETCORE_ENVIRONMENT=Release -p VANITY_URL=${VANITY_URL}-p SUFFIX='-${BRANCH_LOWER}'"
                    echo "Building ..."
                    sh "./player.sh build database dev -p SUFFIX='-${BRANCH_LOWER}'"
                    sh "./player.sh build api dev ${API_ARGS}"
                    sh "./player.sh build frontend dev ${FRONTEND_ARGS}"
                    }
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
                    if (env.BRANCH_NAME == 'develop') {
                    FRONTEND_ARGS="-p REDIRECT_URL=${SCHEMA}://${VANITY_URL} -p VANITY_URL=${VANITY_URL}"
                    API_ARGS="-p ASPNETCORE_ENVIRONMENT=Release -p VANITY_URL=${VANITY_URL}"
                    echo "Deploy to dev..."
                    sh "./player.sh deploy database dev"
                    sh "./player.sh deploy api dev ${API_ARGS}"
                    sh "./player.sh deploy frontend dev ${FRONTEND_ARGS}"
                    } else {
                    FRONTEND_ARGS="-p REDIRECT_URL=${SCHEMA}://${VANITY_URL} -p VANITY_URL=${VANITY_URL} -p SUFFIX='-${BRANCH_LOWER}'"
                    API_ARGS="-p ASPNETCORE_ENVIRONMENT=Release -p VANITY_URL=${VANITY_URL}-p SUFFIX='-${BRANCH_LOWER}'"
                    echo "Deploy to dev..."
                    sh "./player.sh deploy database dev -p SUFFIX='-${BRANCH_LOWER}'"
                    sh "./player.sh deploy api dev ${API_ARGS}"
                    sh "./player.sh deploy frontend dev ${FRONTEND_ARGS}"
                    }
                }
            }
        }
        stage('SchemaSpy Database Investigation') {
            when { expression { ( GIT_BRANCH == 'develop' ) } }
            agent { label 'master' }
            steps {
                sh "./player.sh toolbelt schemaspy dev -p SOURCE_CONTEXT_DIR='schemaspy'"
            }
        }
    }
}
