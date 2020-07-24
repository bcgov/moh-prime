pipeline {
    agent none
    environment {
        BRANCH_LOWER=BRANCH_NAME.toLowerCase()
        VANITY_URL="${BRANCH_LOWER}.pharmanetenrolment.gov.bc.ca"
        SCHEMA="https"
        PORT="8443"
        SUFFIX="-${BRANCH_LOWER}"
        FRONTEND_ARGS="-p REDIRECT_URL=${SCHEMA}://${VANITY_URL} -p VANITY_URL=${VANITY_URL}"
        API_ARGS="-p ASPNETCORE_ENVIRONMENT=Release -p VANITY_URL=${VANITY_URL}"
        // TODO request made for adding human-readable ID to moh-prime credential of jenkins-git-credential
        // to reduce changes required if credentials change similar to the access token credential I created
        GITHUB_CREDENTIAL = credentials('42118129-086b-40a0-b800-bf490c2a2e82')
    }
    options {
        disableResume()
    }
    stages {
        stage('Checkout') {
            agent { label 'master' }
            steps {
                script {
                    checkout scm
                    sh "./player.sh sparsify"
                }
            }
        }
        stage('Build') {
            options {
                timeout(time: 90, unit: 'MINUTES') // timeout on this stage
            }
            agent { label 'master' }
            steps {
                script {
                  sh "./player.sh notifyGitHub pending build $GITHUB_CREDENTIAL"
                  echo "Building ..."
                  sh "./player.sh build api dev ${API_ARGS} -p SUFFIX=${SUFFIX}"
                  sh "./player.sh build frontend dev ${FRONTEND_ARGS} -p SUFFIX=${SUFFIX}"
                  sh "./player.sh build document-manager dev -p SUFFIX=${SUFFIX}"
                }
            }
            post {
              success {
                sh "./player.sh notifyGitHub success build $GITHUB_CREDENTIAL"
              }
              failure {
                sh "./player.sh notifyGitHub failure build $GITHUB_CREDENTIAL"
              }
            }
        }
        stage('Deploy (PR)') {
            options {
                timeout(time: 10, unit: 'MINUTES') // timeout on this stage
            }
            when { expression { ( GIT_BRANCH != 'develop' ) } }
            agent { label 'master' }
            steps {
                script {
                    sh "./player.sh notifyGitHub pending deployment $GITHUB_CREDENTIAL"

                    echo "Deploy to dev..."
                    sh "./player.sh deploy redis dev -p SUFFIX=${SUFFIX}"
                    sh "./player.sh deploy postgres-ephemeral dev -p SUFFIX=${SUFFIX} -p VOLUME_CAPACITY=256Mi"
                    sh "./player.sh deploy document-manager-ephemeral dev -p SUFFIX=${SUFFIX} -p VANITY_URL=${VANITY_URL}" 
                    // sh "./player.sh deploy mongo-ephemeral dev -p SUFFIX=${SUFFIX} -p VOLUME_CAPACITY=256Mi"
                    sh "./player.sh deploy api dev ${API_ARGS} -p SUFFIX=${SUFFIX}"
                    sh "./player.sh deploy frontend dev ${FRONTEND_ARGS} -p SUFFIX=${SUFFIX}"
                }
            }
            post {
              success {
                sh "./player.sh notifyGitHub success deployment $GITHUB_CREDENTIAL"
              }
              failure {
                sh "./player.sh notifyGitHub failure deployment $GITHUB_CREDENTIAL"
              }
            }
        }
        stage('Deploy (DEV)') {
            options {
                timeout(time: 10, unit: 'MINUTES') // timeout on this stage
            }
            when { expression { ( GIT_BRANCH == 'develop' ) } }
            agent { label 'master' }
            steps {
                script {
                    echo "Deploy to dev..."
                    sh "./player.sh deploy redis dev -p SUFFIX=${SUFFIX}"
                    sh "./player.sh deploy postgres dev -p SUFFIX=${SUFFIX} -p VOLUME_CAPACITY=1Gi"
                    sh "./player.sh deploy document-manager dev -p SUFFIX=${SUFFIX} -p VANITY_URL=${VANITY_URL} -p VOLUME_CAPACITY=1Gi"
                    // sh "./player.sh deploy mongo dev -p SUFFIX=${SUFFIX} -p VOLUME_CAPACITY=1Gi"
                    sh "./player.sh deploy api dev ${API_ARGS} -p SUFFIX=${SUFFIX}"
                    sh "./player.sh deploy frontend dev ${FRONTEND_ARGS} -p SUFFIX=${SUFFIX}"
                }
            }
            post {
              success {
                sh "./player.sh notifyGitHub success deployment $GITHUB_CREDENTIAL"
              }
              failure {
                sh "./player.sh notifyGitHub failure deployment $GITHUB_CREDENTIAL"
              }
            }
        }
        // TODO requires an update to Jenkins and addition of official SonarQube Jenkins plugin
        stage('Integrity Test (PR)') {
          agent { label 'master' }
          when { expression { (BRANCH_NAME ==~ /PR-\d+/) }; }
          steps {
        //     sh "./player.sh notifyGitHub pending continuous-integration/jenkins/integrity-test $GITHUB_CREDENTIAL"
        //
        //     timeout(time: 10, unit: 'MINUTES') {
        //       withSonarQubeEnv('SonarQube Server') {
                sh "./player.sh scan" // ???
        //       }
        //     }
          }
        }
        // stage('Integrity Test Gate (PR)') {
        //   agent { label 'master' }
        //   when { expression { (BRANCH_NAME ==~ /PR-\d+/) }; }
        //   steps {
        //     timeout(time: 1, unit: 'HOURS') {
        //       // Abort the pipeline if quality gate status is not green
        //       waitForQualityGate abortPipeline = true
        //     }
        //   }
        //   post {
        //     success {
        //       sh "./player.sh notifyGitHub success continuous-integration/jenkins/tests $GITHUB_CREDENTIAL"
        //     }
        //     failure {
        //       sh "./player.sh notifyGitHub failure continuous-integration/jenkins/tests $GITHUB_CREDENTIAL"
        //     }
        //   }
        // }
        stage('Quality Check') {
            options {
                timeout(time: 30, unit: 'MINUTES') // timeout on this stage
            }
            when { expression { ( BRANCH_NAME == 'develop' ) } }
            parallel {
                stage('SonarQube') {
                    agent { label 'code-tests' }
                    steps {
                        sh "./player.sh scan"
                    }
                }
                stage('Zap') {
                    agent { label 'code-tests' }
                    steps {
                        sh "./player.sh zap frontend"
                    }
                }
                stage('SchemaSpy') {
                    agent { label 'master' }
                    steps {
                        sh "./player.sh toolbelt schemaspy dev"
                    }
                }
            }
        }
        stage('Cleanup') {
             steps {
                 agent { label 'code-tests' }
                 script {
                     sh "./player.sh sparsify"
                }
            }
        }
    }
}
