pipeline {
    agent none
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
                echo "Branch Name='${BRANCH_NAME}'"
                sh "./player.sh build database dev"
                sh "./player.sh build api dev"
                sh "./player.sh build frontend dev '-p URL_PREFIX=${BRANCH_NAME}'"
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
                sh "./player.sh deploy api dev"
                sh "./player.sh deploy frontend dev '-p URL_PREFIX=${BRANCH_NAME}'"
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
