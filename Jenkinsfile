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
                    // echo "PROJECT_OWNER: $PROJECT_OWNER"
                    // echo "PROJECT_NAME: $PROJECT_NAME"
                    // echo "GIT_COMMENT: $GIT_COMMIT"
                    // echo "BUILD_NUMBER: $BUILD_NUMBER"
                    // Attempt to get a message into GitHub
                    sh "./player.sh preventMerge"
                }
            }
        }
        stage('Build Branch') {
            options {
                timeout(time: 90, unit: 'MINUTES') // timeout on this stage
            }
            agent { label 'master' }
            steps {
                script {
                    echo "Building ..."
                    sh "./player.sh build api dev ${API_ARGS} -p SUFFIX=${SUFFIX}"
                    sh "./player.sh build frontend dev ${FRONTEND_ARGS} -p SUFFIX=${SUFFIX}"
                    sh "./player.sh build document-manager dev -p SUFFIX=${SUFFIX}"
                }
            }
        }
        stage('Deploy Images') {
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
                    sh "./player.sh deploy document-manager dev -p SUFFIX=${SUFFIX} -p VOLUME_CAPACITY=1Gi"
                    // sh "./player.sh deploy mongo dev -p SUFFIX=${SUFFIX} -p VOLUME_CAPACITY=1Gi"
                    sh "./player.sh deploy api dev ${API_ARGS} -p SUFFIX=${SUFFIX}"
                    sh "./player.sh deploy frontend dev ${FRONTEND_ARGS} -p SUFFIX=${SUFFIX}"
                }
            }
        }
        stage('Deploy PR') {
            options {
                timeout(time: 10, unit: 'MINUTES') // timeout on this stage
            }
            when { expression { ( GIT_BRANCH != 'develop' ) } }
            agent { label 'master' }
            steps {
                script {
                    echo "Deploy to dev..."
                    sh "./player.sh deploy redis dev -p SUFFIX=${SUFFIX}"
                    sh "./player.sh deploy postgres-ephemeral dev -p SUFFIX=${SUFFIX} -p VOLUME_CAPACITY=256Mi"
                    sh "./player.sh deploy document-manager-ephemeral dev -p SUFFIX=${SUFFIX}"
                    // sh "./player.sh deploy mongo-ephemeral dev -p SUFFIX=${SUFFIX} -p VOLUME_CAPACITY=256Mi"
                    sh "./player.sh deploy api dev ${API_ARGS} -p SUFFIX=${SUFFIX}"
                    sh "./player.sh deploy frontend dev ${FRONTEND_ARGS} -p SUFFIX=${SUFFIX}"
                }
            }
        }
        stage('Integrity Test') {
          options {
              timeout(time: 10, unit: 'MINUTES') // timeout on this stage
          }
          agent { label 'master' }
          steps {
            script {
              echo "Running integrity tests..."
              echo "$GIT_BRANCH"
              echo "$BRANCH_NAME"
              // Attempt to see merge prevented
              // exit 1;

              // sh "exit 1"
            }
            // post {
            //   always: {
            //     echo "Tests are always run!"
            //   }
            //   success: {
            //     echo: "Tests Passed :)"
            //   }
            //   failure: {
            //     echo: "Tests Failed :("
            //   }
            // }
          }
        }
        stage('Quality Check') {
            options {
                timeout(time: 30, unit: 'MINUTES') // timeout on this stage
            }
            when { expression { ( BRANCH_NAME == 'develop' ) } }
            parallel {
                stage('SonarQube Code Check') {
                    agent { label 'code-tests' }
                    steps {
                        sh "./player.sh scan"
                    }
                }
                stage('ZAP') {
                    agent { label 'code-tests' }
                    steps {
                        echo "Scanning..."
                        sh "./player.sh zap frontend"
                    }
                }
                stage('SchemaSpy Database Investigation') {
                    agent { label 'master' }
                    steps {
                        sh "./player.sh toolbelt schemaspy dev"
                    }
                }
            }
        }
        stage('Prevent Merge') {
          options {
              timeout(time: 10, unit: 'MINUTES')
          }
          // when { expression { ( BRANCH_NAME == 'feature/PRIME-895-api-unit-test' ) }  }
          steps {
            script {
              sh "exit 1"
            }
          }
        }
        // stage('Cleanup') {
        //     steps {
        //         script {
        //             sh "./player.sh sparsify"
        //         }
        //     }
        // }
    }
}
