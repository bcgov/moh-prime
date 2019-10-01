app {
    name = 'moh-prime'
    version = 'snapshot'

    namespaces {
        'build'{
            namespace = 'dqszvc-tools'
            disposable = true
        }
        'dev' {
            namespace = 'dqszvc-dev'
            disposable = true
        }
    }

    git {
        workDir = ['git', 'rev-parse', '--show-toplevel'].execute().text.trim()
        uri = ['git', 'config', '--get', 'remote.origin.url'].execute().text.trim()
        commit = ['git', 'rev-parse', 'HEAD'].execute().text.trim()
        changeId = "${opt.'pr'}"
        ref = opt.'branch'?:"refs/pull/${git.changeId}/head"
        github {
            owner = app.git.uri.tokenize('/')[2]
            name = app.git.uri.tokenize('/')[3].tokenize('.git')[0]
        }
    }

    build {
        env {
            name = 'build'
            id = "pr-${app.git.changeId}"
        }
        id = "${app.name}-${app.build.env.name}-${app.build.env.id}"
        name = "${app.name}"
        version = "${app.build.env.name}-${app.build.env.id}"

        suffix = "-${app.git.changeId}"
        namespace = 'dqszvc-tools'
    }

    deployment {
        env {
            name = vars.deployment.env.name // env-name
            id = "pr-${app.git.changeId}"
        }

        id = "${app.name}-${app.deployment.env.name}-${app.deployment.env.id}"
        name = "${app.name}"
        version = "${app.deployment.env.name}-${app.deployment.env.id}"

        namespace = "${vars.deployment.namespace}"
        timeoutInSeconds = 60*20 // 20 minutes
        templates = [
                [
                    'file':'openshift/postgresql.dc.json',
                    'params':[
                            'NAME':"prime-postgresql",
                            'SUFFIX':"${vars.deployment.suffix}",
                            'DATABASE_SERVICE_NAME':"prime-postgresql${vars.deployment.suffix}",
                            'CPU_REQUEST':"${vars.resources.postgres.cpu_request}",
                            'CPU_LIMIT':"${vars.resources.postgres.cpu_limit}",
                            'MEMORY_REQUEST':"${vars.resources.postgres.memory_request}",
                            'MEMORY_LIMIT':"${vars.resources.postgres.memory_limit}",
                            'IMAGE_STREAM_NAMESPACE':'dqszvc-dev',
                            'IMAGE_STREAM_NAME':"prime-postgresql",
                            'IMAGE_STREAM_VERSION':"${app.deployment.version}",
                            'POSTGRESQL_DATABASE':'prime',
                            'VOLUME_CAPACITY':"${vars.DB_PVC_SIZE}"
                    ]
                ],
        ]
    }
}

environments {
    'dev' {
        vars {
            DB_PVC_SIZE = '1Gi'
            DOCUMENT_PVC_SIZE = '1Gi'
            LOG_PVC_SIZE = '1Gi'
            git {
                changeId = "${opt.'pr'}"
            }
            /*
            keycloak {
                clientId_core = "prime-application-dev"
                clientId_primespace = "primespace-dev"
                resource = "prime-application-dev"
                idpHint_core = "dev"
                idpHint_primespace = "dev"
                url = "https://sso-test.pathfinder.gov.bc.ca/auth"
                known_config_url = "https://sso-test.pathfinder.gov.bc.ca/auth/realms/prime/.well-known/openid-configuration"
                siteminder_url = "https://logontest.gov.bc.ca"
            }
            */
            resources {
                postgres {
                    cpu_request = "50m"
                    cpu_limit = "100m"
                    memory_request = "256Mi"
                    memory_limit = "512Mi"
                }
            }
            deployment {
                env {
                    name = "dev"
                }
                key = 'dev'
                namespace = 'dqszvc-dev'
                suffix = "-pr-${vars.git.changeId}"
                application_suffix = "-pr-${vars.git.changeId}"
                node_env = "development"
                map_portal_id = "e926583cd0114cd19ebc591f344e30dc"
                elastic_enabled_core = 0
                elastic_enabled_nris = 0
                elastic_service_name = "Prime Dev"
                elastic_service_name_nris = "NRIS API Dev"
                elastic_service_name_docman = 'DocMan Dev'
            }
            modules {
                'prime-python-backend' {
                    HOST = "http://prime-python-backend${vars.deployment.suffix}:5000"
                    PATH = "/${vars.git.changeId}/api"
                }
            }
        }
    }
}
