pipeline {
    agent none
    options {
        disableResume()
    }
    stages {
        stage('Build') {
            agent { label 'master' }
            steps {
                echo "Building ..."
                sh "./player.sh build database dev"
                sh "./player.sh build api dev"
                sh "./player.sh build frontend dev"
            }
        }
        stage('Deploy') {
            agent { label 'master' }
            steps {
                echo "Deploying..."
                sh "./player.sh deploy database dev"
                sh "./player.sh deploy api dev"
                sh "./player.sh deploy frontend dev"
            }
        }
/*
        stage('Build & Deploy Tools') {
            agent { label 'master' }
            steps {
                echo "Assemble toolbelt..."
                //sh "./player.sh toolbelt sonar"
                sh "./player.sh toolbelt mailhog"
            }
        }
*/
        stage('Code Quality Check') {
            agent { label 'code-tests' }
            steps {
                sh "./player.sh scan"
            }
        }
/*
        stage('Test') {
            agent { label 'master' }
            script {
                    def IS_APPROVED = input(message: "Deploy to TEST?", ok: "yes", parameters: [string(name: 'IS_APPROVED', defaultValue: 'yes', description: 'Deploy to TEST?')])
                    if (IS_APPROVED != 'yes') {
                        currentBuild.result = "ABORTED"
                        error "User cancelled"
                    }
            when {
                environment name: 'CHANGE_TARGET', value: 'master'
            }
            steps {
                echo "Test (DEV) ..."
                sh "./player.sh build database dev"
                sh "./player.sh build api dev"
                sh "./player.sh build frontend dev"
                sh "./player.sh deploy database dev"
                sh "./player.sh deploy api dev"
                sh "./player.sh deploy frontend dev"
            }
        }/*
        stage('Deploy (PROD)') {
            agent { label 'master' }
            when {
                environment name: 'CHANGE_TARGET', value: 'master'
            }
            steps {
                script {
                    def IS_APPROVED = input(message: "Deploy to PROD?", ok: "yes", parameters: [string(name: 'IS_APPROVED', defaultValue: 'yes', description: 'Deploy to PROD?')])
                    if (IS_APPROVED != 'yes') {
                        currentBuild.result = "ABORTED"
                        error "User cancelled"
                    }
                    echo "Deploy (PROD)"
                    sh "./player.sh "
                }
            }
        }/*
        stage('Merge to master') {
            agent { label 'master' }
            when {
                environment name: 'CHANGE_TARGET', value: 'master'
            }
            steps {
                script {
                    def IS_APPROVED = input(message: "Merge to master?", ok: "yes", parameters: [string(name: 'IS_APPROVED', defaultValue: 'yes', description: 'Merge to master?')])
                    if (IS_APPROVED != 'yes') {
                        currentBuild.result = "ABORTED"
                        error "User cancelled"
                    }
                    echo "Squashing commits and merging to master"
                }
                withCredentials([usernamePassword(credentialsId: 'github-account', passwordVariable: 'GIT_PASSWORD', usernameVariable: 'GIT_USERNAME')]) {
                    sh '
                        # Update master with latest changes from develop
                        git checkout master
                        git fetch
                        git merge --squash origin/develop
                        git commit -m "Merge branch develop into master"
                        git push https://${GIT_USERNAME}:${GIT_PASSWORD}@github.com/bcgov/moh-prime.git

                        # Update the HEAD on develop to be the same as master
                        git checkout develop
                        git fetch
                        git merge -s ours -m "Updating develop with master" origin/master
                        git push https://${GIT_USERNAME}:${GIT_PASSWORD}@github.com/bcgov/moh-prime.git
                    '
                }
            }
        }*/
        /*
        stage('Acceptance') {
            agent { label 'master' }
            input {
                message "Should we continue with cleanup?"
                ok "Yes!"
            }
            steps {
                echo "Acceptance ..."
                sh "./player cleanup ${CHANGE_ID}"
            }
        }*/
    }
}
