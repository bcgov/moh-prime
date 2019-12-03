pipeline {
    agent none
    options {
        disableResume()
    }
    stages {
        stage('Build Branch') {
            when { expression { ( GIT_BRANCH != 'master' ) } }
            agent { label 'master' }
            steps {
                echo "Building ..."
                sh "./player.sh build database dev"
                sh "./player.sh build api dev"
                sh "./player.sh build frontend dev"
            }
        }
        stage('Deploy Branch') {
            when { expression { ( GIT_BRANCH != 'master' ) } }
            agent { label 'master' }
            steps {
                echo "Deploy to dev..."
                sh "./player.sh deploy database dev"
                sh "./player.sh deploy api dev"
                sh "./player.sh deploy frontend dev"
            }
        }
        stage('SonarQube Code Check') {
            when { expression { ( GIT_BRANCH != 'master' ) } }
            agent { label 'code-tests' }
            steps {
                sh "./player.sh scan"
            }
        }
    }
}
