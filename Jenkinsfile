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
        GITHUB_CREDENTIAL = credentials('jenkins-github-credentials')
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

                    // Access via scoped credentials
                    withCredentials([usernameColonPassword(credentialsId: 'jenkins-github-credentials', variable: 'GITHUB_CREDENTIALV2')]) {
                        sh "./player.sh notifyGitHub pending continuous-integration/jenkins $GITHUB_CREDENTIALV2"
                    }
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
                    notifyGitHub("failure", "continuous-integration/jenkins")
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

              // Access via global credentials
              sh "./player.sh notifyGitHub success continuous-integration/jenkins $GITHUB_CREDENTIAL"
            }
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
        // stage('Cleanup') {
        //     steps {
        //         script {
        //             sh "./player.sh sparsify"
        //         }
        //     }
        // }
    }
}

// @description
// Notify GitHub of a change in the commit status for a specific context.
// @param $2 state: 'pending' | 'success' | 'failure' | 'error'
// @param $3 context: 'continuous-integration/jenkins/example'
// @param $4 GitHub credentials
def notifyGitHub(String state, String context) {
  withCredentials([usernameColonPassword(credentialsId: 'jenkins-github-credentials', variable: 'GITHUB_CREDENTIAL')]) {
    sh("""
      curl \
        -X POST \
        -H "Accept: application/vnd.github.v3+json" \
        -u "${GITHUB_CREDENTIAL}" \
        "https://api.github.com/repos/${PROJECT_OWNER}/${PROJECT_NAME}/statuses/${GIT_COMMIT}" \
        -d "{\"state\": \"${2}\",\"context\": \"${3}\", \"description\": \"Jenkins\", \"target_url\": \"https://jenkins-prod-dqszvc-tools.pathfinder.gov.bc.ca/job/Development/jenkins/Development/job/${BRANCH_NAME}/${BUILD_NUMBER}/display/redirect\"}"
    """)
  }
}
