pipeline {
    agent none
    options {
        disableResume()
    }
    stages {
        stage('Build Branch') {
            agent { label 'master' }
            steps {
                echo "Building ..."
                sh "./player.sh build database dev"
                sh "./player.sh build api dev"
                sh "./player.sh build frontend dev"
            }
        }
        stage('Deploy Branch') {
            agent { label 'master' }
            steps {
                echo "Deploy (DEV) ..."
                sh "./player.sh deploy database dev"
                sh "./player.sh deploy api dev"
                sh "./player.sh deploy frontend dev"
            }
        }
        stage('SonarQube Code Check') {
            agent { label 'code-tests' }
            steps {
                sh "./player.sh scan"
            }
        }
        stage('Cleanup Branch') {
        options {
            timeout(time: 5, unit: 'MINUTES')   // timeout on this stage
        }
            when { expression { ( GIT_BRANCH != 'develop' ) } }
            agent { label 'master' }
            steps {
                script {
                        def IS_APPROVED = input(
                            id: 'IS_APPROVED', message: "Cleanup OpenShift Environment for ${BRANCH_NAME}?", ok: "yes", parameters: [
                                string(name: 'IS_APPROVED', defaultValue: 'yes', description: 'Cleanup OpenShift Environment for branch?')
                                ])
                        if (IS_APPROVED != 'yes') {
                            currentBuild.result = "SUCCESS"
                            echo "User cancelled cleanup"
                        }
                    echo "Test (DEV) ..."
                    sh "./player.sh cleanup ${BRANCH_NAME}"
                }
            }
        }
    }
}
