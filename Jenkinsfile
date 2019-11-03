pipeline {
    agent none
    options {
        disableResume()
    }
    stages {/*
        stage('Build') {
            agent { label 'master' }
            steps {
                echo "Building ..."
                sh "./player.sh build database dev"
                sh "./player.sh build api dev"
                sh "./player.sh build frontend dev"
            }
        }
        stage('Deploy (DEV)') {
            agent { label 'master' }
            steps {
                echo "Deploy (DEV) ..."
                sh "./player.sh deploy database dev"
                sh "./player.sh deploy api dev"
                sh "./player.sh deploy frontend dev"
            }
        }
        /*
        stage('SonarQube analysis') {
        agent { label 'master' }
        steps { 
                sh "${scannerHome}/bin/sonar-scanner -X"
            }
        }
        // This is the work on the current branch
        */
        stage('Code Quality Check') {
            agent { label 'code-tests' }
            steps {
                echo "Deploy (DEV) ..."
                //sh "export OC_APP=dev"
                sh "./player.sh sonar tools"
                //sh "./player.sh sonar dotnet-webapi dev"
                //sh "./player.sh sonar angular-frontend dev"
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
                sh "bash ./player.sh ocApply build postgresql dev"
                sh "bash ./player.sh ocApply build dotnet-webapi dev"
                sh "bash ./player.sh ocApply build angular-frontend dev"
                sh "bash ./player.sh ocApply deploy postgresql dev"
                sh "bash ./player.sh ocApply deploy dotnet-webapi dev"
                sh "bash ./player.sh ocApply deploy angular-frontend dev"
            }
        }
        */
        /*
        stage('Unit Tests and SonarQube Reporting (DEV)') {
            agent { label 'master' }
            steps {
                echo "Running unit tests and reporting them to SonarQube ..."
                sh "unset JAVA_OPTS; pipeline/gradlew --no-build-cache --console=plain --no-daemon -b pipeline/build.gradle cd-unit-test -Pargs.--config=pipeline/config-dev.groovy -Pargs.--pr=${CHANGE_ID} -Pargs.--env=dev -Pargs.--branch=${CHANGE_BRANCH}"
            }
        }
        stage('Functional Test (DEV)') {
            agent { label 'master' }
            steps {
                echo "Functional Test (DEV) ..."
                sh "unset JAVA_OPTS; pipeline/gradlew --no-build-cache --console=plain --no-daemon -b pipeline/build.gradle cd-functional-test -Pargs.--config=pipeline/config-dev.groovy -Pargs.--pr=${CHANGE_ID} -Pargs.--env=dev"
            }
        }
        stage ('ZAP (DEV)'){
            agent { label 'master' }
            steps {
                echo "ZAP (DEV)"
                sh "unset JAVA_OPTS; pipeline/gradlew --no-build-cache --console=plain --no-daemon -b pipeline/build.gradle cd-zap -Pargs.--config=pipeline/config-dev.groovy -Pargs.--pr=${CHANGE_ID} -Pargs.--env=dev"
            }
        }
        stage('Deploy (TEST)') {
            agent { label 'master' }
            when {
                environment name: 'CHANGE_TARGET', value: 'master'
            }
            steps {
                echo "Deploy (TEST)"
                sh "unset JAVA_OPTS; pipeline/gradlew --no-build-cache --console=plain --no-daemon -b pipeline/build.gradle cd-deploy -Pargs.--config=pipeline/config-test.groovy -Pargs.--pr=${CHANGE_ID} -Pargs.--env=test"
            }
        }
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
                    sh "unset JAVA_OPTS; pipeline/gradlew --no-build-cache --console=plain --no-daemon -b pipeline/build.gradle cd-deploy -Pargs.--config=pipeline/config-prod.groovy -Pargs.--pr=${CHANGE_ID} -Pargs.--env=prod"
                }
            }
        }
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
                    sh """
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
                    """
                }
            }
        }
        stage('Acceptance') {
            agent { label 'master' }
            input {
                message "Should we continue with cleanup?"
                ok "Yes!"
            }
            steps {
                echo "Acceptance ..."
                sh "unset JAVA_OPTS; pipeline/gradlew --no-build-cache --console=plain --no-daemon -b pipeline/build.gradle cd-clean -Pargs.--config=pipeline/config-dev.groovy -Pargs.--pr=${CHANGE_ID}"
            }
        }
        */
    }
}