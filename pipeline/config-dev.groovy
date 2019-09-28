app {
    name = 'moh-prime'
    version = 'snapshot'

    namespaces {
        'build'{
            namespace = 'empr-moh-prime-tools'
            disposable = true
        }
        'dev' {
            namespace = 'empr-moh-prime-dev'
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
        namespace = 'empr-moh-prime-tools'
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
                            'NAME':"moh-prime-postgresql",
                            'SUFFIX':"${vars.deployment.suffix}",
                            'DATABASE_SERVICE_NAME':"moh-prime-postgresql${vars.deployment.suffix}",
                            'CPU_REQUEST':"${vars.resources.postgres.cpu_request}",
                            'CPU_LIMIT':"${vars.resources.postgres.cpu_limit}",
                            'MEMORY_REQUEST':"${vars.resources.postgres.memory_request}",
                            'MEMORY_LIMIT':"${vars.resources.postgres.memory_limit}",
                            'IMAGE_STREAM_NAMESPACE':'',
                            'IMAGE_STREAM_NAME':"moh-prime-postgresql",
                            'IMAGE_STREAM_VERSION':"${app.deployment.version}",
                            'POSTGRESQL_DATABASE':'moh-prime',
                            'VOLUME_CAPACITY':"${vars.DB_PVC_SIZE}"
                    ]
                ],
                [
                    'file':'openshift/redis.dc.json',
                    'params':[
                            'NAME':"moh-prime-redis",
                            'DATABASE_SERVICE_NAME':"moh-prime-redis${vars.deployment.suffix}",
                            'CPU_REQUEST':"${vars.resources.redis.cpu_request}",
                            'CPU_LIMIT':"${vars.resources.redis.cpu_limit}",
                            'MEMORY_REQUEST':"${vars.resources.redis.memory_request}",
                            'MEMORY_LIMIT':"${vars.resources.redis.memory_limit}",
                            'REDIS_VERSION':"3.2"
                    ]
                ],
                [
                    'file':'openshift/_nodejs.dc.json',
                    'params':[
                            'NAME':"moh-prime-frontend",
                            'SUFFIX': "${vars.deployment.suffix}",
                            'APPLICATION_SUFFIX': "${vars.deployment.application_suffix}",
                            'TAG_NAME':"${app.deployment.version}",
                            'PORT':3000,
                            'CPU_REQUEST':"${vars.resources.node.cpu_request}",
                            'CPU_LIMIT':"${vars.resources.node.cpu_limit}",
                            'MEMORY_REQUEST':"${vars.resources.node.memory_request}",
                            'MEMORY_LIMIT':"${vars.resources.node.memory_limit}",
                            'REPLICA_MIN':"${vars.resources.node.replica_min}",
                            'REPLICA_MAX':"${vars.resources.node.replica_max}",
                            'APPLICATION_DOMAIN': "${vars.modules.'moh-prime-frontend'.HOST}",
                            'BASE_PATH': "${vars.modules.'moh-prime-frontend'.PATH}",
                            'NODE_ENV': "${vars.deployment.node_env}",
                            'MAP_PORTAL_ID': "${vars.deployment.map_portal_id}",
                            'KEYCLOAK_RESOURCE': "${vars.keycloak.resource}",
                            'KEYCLOAK_CLIENT_ID': "${vars.keycloak.clientId_core}",
                            'KEYCLOAK_URL': "${vars.keycloak.url}",
                            'KEYCLOAK_IDP_HINT': "${vars.keycloak.idpHint_core}",
                            'API_URL': "https://${vars.modules.'moh-prime-nginx'.HOST_CORE}${vars.modules.'moh-prime-nginx'.PATH}/api",
                            'DOCUMENT_MANAGER_URL': "https://${vars.modules.'moh-prime-nginx'.HOST_CORE}${vars.modules.'moh-prime-nginx'.PATH}/document-manager"
                    ]
                ],
                [
                    'file':'openshift/_nodejs.dc.json',
                    'params':[
                            'NAME':"moh-prime-frontend-public",
                            'SUFFIX': "${vars.deployment.suffix}",
                            'APPLICATION_SUFFIX': "${vars.deployment.application_suffix}",
                            'TAG_NAME':"${app.deployment.version}",
                            'PORT':3020,
                            'CPU_REQUEST':"${vars.resources.node.cpu_request}",
                            'CPU_LIMIT':"${vars.resources.node.cpu_limit}",
                            'MEMORY_REQUEST':"${vars.resources.node.memory_request}",
                            'MEMORY_LIMIT':"${vars.resources.node.memory_limit}",
                            'REPLICA_MIN':"${vars.resources.node.replica_min}",
                            'REPLICA_MAX':"${vars.resources.node.replica_max}",
                            'APPLICATION_DOMAIN': "${vars.modules.'moh-prime-frontend-public'.HOST}",
                            'BASE_PATH': "${vars.modules.'moh-prime-frontend-public'.PATH}",
                            'NODE_ENV': "${vars.deployment.node_env}",
                            'KEYCLOAK_RESOURCE': "${vars.keycloak.resource}",
                            'KEYCLOAK_CLIENT_ID': "${vars.keycloak.clientId_minespace}",
                            'KEYCLOAK_URL': "${vars.keycloak.url}",
                            'KEYCLOAK_IDP_HINT': "${vars.keycloak.idpHint_minespace}",
                            'SITEMINDER_URL': "${vars.keycloak.siteminder_url}",
                            'API_URL': "https://${vars.modules.'moh-prime-nginx'.HOST_CORE}${vars.modules.'moh-prime-nginx'.PATH}/api",
                            'DOCUMENT_MANAGER_URL': "https://${vars.modules.'moh-prime-nginx'.HOST_CORE}${vars.modules.'moh-prime-nginx'.PATH}/document-manager"

                    ]
                ],
                [
                    'file':'openshift/_nginx.dc.json',
                    'params':[
                            'NAME':"moh-prime-nginx",
                            'SUFFIX': "${vars.deployment.suffix}",
                            'VERSION':"${app.deployment.version}",
                            'LOG_PVC_SIZE':"${vars.LOG_PVC_SIZE}",
                            'CPU_REQUEST':"${vars.resources.nginx.cpu_request}",
                            'CPU_LIMIT':"${vars.resources.nginx.cpu_limit}",
                            'MEMORY_REQUEST':"${vars.resources.nginx.memory_request}",
                            'MEMORY_LIMIT':"${vars.resources.nginx.memory_limit}",
                            'REPLICA_MIN':"${vars.resources.nginx.replica_min}",
                            'REPLICA_MAX':"${vars.resources.nginx.replica_max}",
                            'CORE_DOMAIN': "${vars.modules.'moh-prime-nginx'.HOST_CORE}",
                            'MINESPACE_DOMAIN': "${vars.modules.'moh-prime-nginx'.HOST_MINESPACE}",
                            'ROUTE': "${vars.modules.'moh-prime-nginx'.ROUTE}",
                            'PATH_PREFIX': "${vars.modules.'moh-prime-nginx'.PATH}",
                            'CORE_SERVICE_URL': "${vars.modules.'moh-prime-frontend'.HOST}",
                            'NRIS_API_SERVICE_URL': "${vars.modules.'moh-prime-nris-backend'.HOST}",
                            'DOCUMENT_MANAGER_SERVICE_URL': "${vars.modules.'moh-prime-docman-backend'.HOST}",
                            'MINESPACE_SERVICE_URL': "${vars.modules.'moh-prime-frontend-public'.HOST}",
                            'API_SERVICE_URL': "${vars.modules.'moh-prime-python-backend'.HOST}",
                    ]
                ],
                [
                    'file':'openshift/_python36.dc.json',
                    'params':[
                            'NAME':"moh-prime-python-backend",
                            'FLYWAY_NAME':"moh-prime-flyway-migration-client",
                            'SUFFIX': "${vars.deployment.suffix}",
                            'VERSION':"${app.deployment.version}",
                            'CPU_REQUEST':"${vars.resources.python.cpu_request}",
                            'CPU_LIMIT':"${vars.resources.python.cpu_limit}",
                            'MEMORY_REQUEST':"${vars.resources.python.memory_request}",
                            'MEMORY_LIMIT':"${vars.resources.python.memory_limit}",
                            'UWSGI_THREADS':"${vars.resources.python.uwsgi_threads}",
                            'UWSGI_PROCESSES':"${vars.resources.python.uwsgi_processes}",
                            'REPLICA_MIN':"${vars.resources.python.replica_min}",
                            'REPLICA_MAX':"${vars.resources.python.replica_max}",
                            'JWT_OIDC_WELL_KNOWN_CONFIG': "${vars.keycloak.known_config_url}",
                            'JWT_OIDC_AUDIENCE': "${vars.keycloak.clientId_core}",
                            'APPLICATION_DOMAIN': "${vars.modules.'moh-prime-python-backend'.HOST}",
                            'BASE_PATH': "${vars.modules.'moh-prime-python-backend'.PATH}",
                            'DB_CONFIG_NAME': "moh-prime-postgresql${vars.deployment.suffix}",
                            'DB_NRIS_CONFIG_NAME': "moh-prime-postgresql${vars.deployment.suffix}-nris",
                            'REDIS_CONFIG_NAME': "moh-prime-redis${vars.deployment.suffix}",
                            'CACHE_REDIS_HOST': "moh-prime-redis${vars.deployment.suffix}",
                            'ELASTIC_ENABLED': "${vars.deployment.elastic_enabled_core}",
                            'ELASTIC_SERVICE_NAME': "${vars.deployment.elastic_service_name}",
                            'ENVIRONMENT_NAME':"${app.deployment.env.name}",
                            'API_URL': "https://${vars.modules.'moh-prime-nginx'.HOST_CORE}${vars.modules.'moh-prime-nginx'.PATH}/api",
                            'NRIS_API_URL': "${vars.modules.'moh-prime-nris-backend'.HOST}${vars.modules.'moh-prime-nris-backend'.PATH}",
                            'DOCUMENT_MANAGER_URL': "${vars.modules.'moh-prime-docman-backend'.HOST}${vars.modules.'moh-prime-docman-backend'.PATH}",
                    ]
                ],
                [
                    'file':'microservices/document_manager/openshift/_python36_docman.dc.json',
                    'params':[
                            'NAME':"moh-prime-docman-backend",
                            'SUFFIX': "${vars.deployment.suffix}",
                            'VERSION':"${app.deployment.version}",
                            'CPU_REQUEST':"${vars.resources.python_lite.cpu_request}",
                            'CPU_LIMIT':"${vars.resources.python_lite.cpu_limit}",
                            'MEMORY_REQUEST':"${vars.resources.python_lite.memory_request}",
                            'MEMORY_LIMIT':"${vars.resources.python_lite.memory_limit}",
                            'UWSGI_THREADS':"${vars.resources.python_lite.uwsgi_threads}",
                            'UWSGI_PROCESSES':"${vars.resources.python_lite.uwsgi_processes}",
                            'REPLICA_MIN':"${vars.resources.python_lite.replica_min}",
                            'REPLICA_MAX':"${vars.resources.python_lite.replica_max}",
                            'JWT_OIDC_WELL_KNOWN_CONFIG': "${vars.keycloak.known_config_url}",
                            'JWT_OIDC_AUDIENCE': "${vars.keycloak.clientId_core}",
                            'APPLICATION_DOMAIN': "${vars.modules.'moh-prime-python-backend'.HOST}",
                            'BASE_PATH': "${vars.modules.'moh-prime-docman-backend'.PATH}",
                            'DB_HOST': "moh-prime-postgresql${vars.deployment.suffix}",
                            'DB_CONFIG_NAME': "moh-prime-postgresql${vars.deployment.suffix}",
                            'REDIS_CONFIG_NAME': "moh-prime-redis${vars.deployment.suffix}",
                            'CACHE_REDIS_HOST': "moh-prime-redis${vars.deployment.suffix}",
                            'ELASTIC_ENABLED': "${vars.deployment.elastic_enabled_core}",
                            'ELASTIC_SERVICE_NAME': "${vars.deployment.elastic_service_name_docman}",
                            'DOCUMENT_CAPACITY':"${vars.DOCUMENT_PVC_SIZE}",
                            'ENVIRONMENT_NAME':"${app.deployment.env.name}",
                            'API_URL': "https://${vars.modules.'moh-prime-nginx'.HOST_CORE}${vars.modules.'moh-prime-nginx'.PATH}/document-manager",
                    ]
                ],
                [
                    'file':'microservices/nris_api/openshift/_python36_oracle.dc.json',
                    'params':[
                            'NAME':"moh-prime-nris-backend",
                            'SUFFIX': "${vars.deployment.suffix}",
                            'VERSION':"${app.deployment.version}",
                            'CPU_REQUEST':"${vars.resources.python_lite.cpu_request}",
                            'CPU_LIMIT':"${vars.resources.python_lite.cpu_limit}",
                            'MEMORY_REQUEST':"${vars.resources.python_lite.memory_request}",
                            'MEMORY_LIMIT':"${vars.resources.python_lite.memory_limit}",
                            'REPLICA_MIN':"${vars.resources.python_lite.replica_min}",
                            'REPLICA_MAX':"${vars.resources.python_lite.replica_max}",
                            'UWSGI_THREADS':"${vars.resources.python_lite.uwsgi_threads}",
                            'UWSGI_PROCESSES':"${vars.resources.python_lite.uwsgi_processes}",
                            'JWT_OIDC_WELL_KNOWN_CONFIG': "${vars.keycloak.known_config_url}",
                            'JWT_OIDC_AUDIENCE': "${vars.keycloak.clientId_core}",
                            'APPLICATION_DOMAIN': "${vars.modules.'moh-prime-nris-backend'.HOST}",
                            'BASE_PATH': "${vars.modules.'moh-prime-nris-backend'.PATH}",
                            'DB_CONFIG_NAME': "moh-prime-postgresql${vars.deployment.suffix}-nris",
                            'REDIS_CONFIG_NAME': "moh-prime-redis${vars.deployment.suffix}",
                            'CACHE_REDIS_HOST': "moh-prime-redis${vars.deployment.suffix}",
                            'DB_HOST': "moh-prime-postgresql${vars.deployment.suffix}",
                            'ELASTIC_ENABLED': "${vars.deployment.elastic_enabled_nris}",
                            'ELASTIC_SERVICE_NAME': "${vars.deployment.elastic_service_name_nris}",
                            'ENVIRONMENT_NAME':"${app.deployment.env.name}",
                            'API_URL': "https://${vars.modules.'moh-prime-nginx'.HOST_CORE}${vars.modules.'moh-prime-nginx'.PATH}/nris_api",
                    ]
                ]
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
            keycloak {
                clientId_core = "mines-application-dev"
                clientId_minespace = "minespace-dev"
                resource = "mines-application-dev"
                idpHint_core = "dev"
                idpHint_minespace = "dev"
                url = "https://sso-test.pathfinder.gov.bc.ca/auth"
                known_config_url = "https://sso-test.pathfinder.gov.bc.ca/auth/realms/moh-prime/.well-known/openid-configuration"
                siteminder_url = "https://logontest.gov.bc.ca"
            }
            resources {
                node {
                    cpu_request = "10m"
                    cpu_limit = "20m"
                    memory_request = "64Mi"
                    memory_limit = "160Mi"
                    replica_min = 1
                    replica_max = 1
                }
                nginx {
                    cpu_request = "10m"
                    cpu_limit = "20m"
                    memory_request = "64Mi"
                    memory_limit = "104Mi"
                    replica_min = 1
                    replica_max = 1
                }
                python {
                    cpu_request = "50m"
                    cpu_limit = "100m"
                    memory_request = "256Mi"
                    memory_limit = "512Mi"
                    uwsgi_threads = 4
                    uwsgi_processes = 2
                    replica_min = 1
                    replica_max = 1
                }
                python_lite {
                    cpu_request = "10m"
                    cpu_limit = "100m"
                    memory_request = "128Mi"
                    memory_limit = "256Mi"
                    uwsgi_threads = 2
                    uwsgi_processes = 1
                    replica_min = 1
                    replica_max = 1
                }
                postgres {
                    cpu_request = "50m"
                    cpu_limit = "100m"
                    memory_request = "256Mi"
                    memory_limit = "512Mi"
                }
                redis {
                    cpu_request = "10m"
                    cpu_limit = "20m"
                    memory_request = "16Mi"
                    memory_limit = "32Mi"
                }
            }
            deployment {
                env {
                    name = "dev"
                }
                key = 'dev'
                namespace = 'empr-moh-prime-dev'
                suffix = "-pr-${vars.git.changeId}"
                application_suffix = "-pr-${vars.git.changeId}"
                node_env = "development"
                map_portal_id = "e926583cd0114cd19ebc591f344e30dc"
                elastic_enabled_core = 0
                elastic_enabled_nris = 0
                elastic_service_name = "moh-prime Dev"
                elastic_service_name_nris = "NRIS API Dev"
                elastic_service_name_docman = 'DocMan Dev'
            }
            modules {
                'moh-prime-frontend' {
                    HOST = "http://moh-prime-frontend${vars.deployment.suffix}:3000"
                    PATH = "/${vars.git.changeId}"
                }
                'moh-prime-frontend-public' {
                    HOST = "http://moh-prime-frontend-public${vars.deployment.suffix}:3020"
                    PATH = "/${vars.git.changeId}"
                }
                'moh-prime-nginx' {
                    HOST_CORE = "minesdigitalservices-${vars.deployment.key}.pathfinder.gov.bc.ca"
                    HOST_MINESPACE = "minespace-${vars.deployment.key}.pathfinder.gov.bc.ca"
                    PATH = "/${vars.git.changeId}"
                    ROUTE = "/${vars.git.changeId}"
                }
                'moh-prime-python-backend' {
                    HOST = "http://moh-prime-python-backend${vars.deployment.suffix}:5000"
                    PATH = "/${vars.git.changeId}/api"
                }
                'moh-prime-nris-backend' {
                    HOST = "http://moh-prime-nris-backend${vars.deployment.suffix}:5500"
                    PATH = "/${vars.git.changeId}/nris-api"
                }
                'moh-prime-docman-backend' {
                    HOST = "http://moh-prime-docman-backend${vars.deployment.suffix}:5001"
                    PATH = "/${vars.git.changeId}/document-manager"
                }
                'moh-prime-redis' {
                    HOST = "http://moh-prime-redis${vars.deployment.suffix}"
                }
            }
        }
    }
}
