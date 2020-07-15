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
        // to reduce changes required if credentials change
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

                  // TODO catch if build fails and notify GitHub of state "failure"
                  // error("Oh the humanity!")
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
                    sh "./player.sh deploy document-manager-ephemeral dev -p SUFFIX=${SUFFIX}"
                    // sh "./player.sh deploy mongo-ephemeral dev -p SUFFIX=${SUFFIX} -p VOLUME_CAPACITY=256Mi"
                    sh "./player.sh deploy api dev ${API_ARGS} -p SUFFIX=${SUFFIX}"
                    sh "./player.sh deploy frontend dev ${FRONTEND_ARGS} -p SUFFIX=${SUFFIX}"

                    // TODO catch if deploy fails and notify GitHub of state "failure"
                    // error("Oh the humanity!")
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
                    sh "./player.sh deploy document-manager dev -p SUFFIX=${SUFFIX} -p VOLUME_CAPACITY=1Gi"
                    // sh "./player.sh deploy mongo dev -p SUFFIX=${SUFFIX} -p VOLUME_CAPACITY=1Gi"
                    sh "./player.sh deploy api dev ${API_ARGS} -p SUFFIX=${SUFFIX}"
                    sh "./player.sh deploy frontend dev ${FRONTEND_ARGS} -p SUFFIX=${SUFFIX}"
                }
            }
        }
        // stage('Integrity Test') {
        //   options {
        //       timeout(time: 10, unit: 'MINUTES') // timeout on this stage
        //   }
        //   agent { label 'master' }
        //   steps {
        //     script {
        //       sh "./player.sh notifyGitHub pending continuous-integration/jenkins/integrity-test $GITHUB_CREDENTIAL"

        //       echo "Running integrity tests..."
        //       echo "$GIT_BRANCH"
        //       echo "$BRANCH_NAME"

        //       sh "./player.sh notifyGitHub success continuous-integration/jenkins/integrity-test $GITHUB_CREDENTIAL"
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
                    // steps {
                    //   withSonarQubeEnv('SonarQube Server') {
                    //     sh 'mvn clean package sonar:sonar'
                    //   }
                    // }
                }
                // post {
                //   success {
                //     sh "./player.sh notifyGitHub success continuous-integration/jenkins/tests $GITHUB_CREDENTIAL"
                //   }
                //   failure {
                //     sh "./player.sh notifyGitHub failure continuous-integration/jenkins/tests $GITHUB_CREDENTIAL"
                //   }
                // }
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
        // TODO need webhooks setup, need Jenkins upgraded, need a drink...
        // stage('Quality Gate') {
        //     options {
        //         timeout(time: 1, unit: 'HOURS') // timeout on this stage
        //     }
        //     steps {
        //       // Abort the pipeline if quality gate status is not green
        //       waitForQualityGate abortPipeline = true
        //     }
        // }
        // stage('Cleanup') {
        //     steps {
        //         script {
        //             sh "./player.sh sparsify"
        //         }
        //     }
        // }
    }
}

// TODO doesn't load project.conf
// notifyGitHub("failure", "example")
// @description
// Notify GitHub of a change in the commit status for a specific context.
// @param $2 state: 'pending' | 'success' | 'failure' | 'error'
// @param $3 context: 'continuous-integration/jenkins/example'
// @param $4 GitHub credentials
// def notifyGitHub(String state, String context) {
//   withCredentials([usernameColonPassword(credentialsId: '42118129-086b-40a0-b800-bf490c2a2e82', variable: 'GITHUB_CREDENTIAL')]) {
//     sh("""
//       . project.conf
//       curl \
//         -X POST \
//         -H "Accept: application/vnd.github.v3+json" \
//         -u "${GITHUB_CREDENTIAL}" \
//         "https://api.github.com/repos/${PROJECT_OWNER}/${PROJECT_NAME}/statuses/${GIT_COMMIT}" \
//         -d "{\"state\": \"${state}\",\"context\": \"${context}\", \"description\": \"Jenkins\", \"target_url\": \"https://jenkins-prod-dqszvc-tools.pathfinder.gov.bc.ca/job/Development/jenkins/Development/job/${BRANCH_NAME}/${BUILD_NUMBER}/display/redirect\"}"
//     """)
//   }
// }
