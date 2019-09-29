app {
    name = 'prime'
    version = 'snapshot'
        namespaces {
        'build'{
            namespace = 'dqszvc-tools'
            disposable = true
        }
        'prod' {
            namespace = 'dqszvc-prod'
            disposable = false
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
                    'file':'pipeline/postgresql.dc.json',
                    'params':[
                            'NAME':"prime-postgresql",
                            'SUFFIX':"${vars.deployment.suffix}",
                            'DATABASE_SERVICE_NAME':"prime-postgresql${vars.deployment.suffix}",
                            'CPU_REQUEST':"${vars.resources.postgres.cpu_request}",
                            'CPU_LIMIT':"${vars.resources.postgres.cpu_limit}",
                            'MEMORY_REQUEST':"${vars.resources.postgres.memory_request}",
                            'MEMORY_LIMIT':"${vars.resources.postgres.memory_limit}",
                            'IMAGE_STREAM_NAMESPACE':'',
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
    'prod' {
        vars {
            DB_PVC_SIZE = '50Gi'
            DOCUMENT_PVC_SIZE = '50Gi'
            BACKUP_VERIFICATION_PVC_SIZE = '10Gi'
            LOG_PVC_SIZE = '5Gi'
            METABASE_PVC_SIZE = '20Gi'
            SCHEDULER_PVC_SIZE = '20Gi'
            git {
                changeId = "${opt.'pr'}"
            }
            resources {
                postgres {
                    cpu_request = "200m"
                    cpu_limit = "1"
                    memory_request = "1.5Gi"
                    memory_limit = "4Gi"
                }
            }
            keycloak {
                clientId_core = "prime-application-prod"
                clientId_primespace = "primespace-prod"
                resource = "prime-application-prod"
                idpHint_core = "idir"
                idpHint_primespace = "bceid"
                url = "https://sso.pathfinder.gov.bc.ca/auth"
                known_config_url = "https://sso.pathfinder.gov.bc.ca/auth/realms/prime/.well-known/openid-configuration"
                siteminder_url = "https://logon.gov.bc.ca"
            }
            deployment {
                env {
                    name = "prod"
                }
                suffix = "-prod"
                application_suffix = "-pr-${vars.git.changeId}"
                key = 'prod'
                namespace = 'dqszvc-prod'
                node_env = "production"
                map_portal_id = "803130a9bebb4035b3ac671aafab12d7"
                elastic_enabled_core = 1
                elastic_enabled_nris = 1
                elastic_service_name = "prime Prod"
                elastic_service_name_nris = "NRIS API Prod"
                elastic_service_name_docman = 'DocMan Prod'
            }
        }
    }
}
