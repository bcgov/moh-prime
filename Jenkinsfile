pipeline {
    agent none
    environment {
        BRANCH_LOWER = BRANCH_NAME.toLowerCase()
        FRONTEND_ARGS = "VANITY_URL=${BRANCH_LOWER}.pathfinder.gov.bc.ca"
        VANITY_URL='${BRANCH_LOWER}.pathfinder.gov.bc.ca'
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
                sh "./player.sh build api dev '${FRONTEND_ARGS}'"
                sh "./player.sh build frontend dev '${FRONTEND_ARGS}'"
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
                sh "./player.sh deploy api dev '${FRONTEND_ARGS}'"
                sh "./player.sh deploy frontend dev '${FRONTEND_ARGS}'"
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
