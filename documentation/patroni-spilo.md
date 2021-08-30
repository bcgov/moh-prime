# Patroni/Spilo PostgreSQL Database Documentation

More information and descriptions of parameters is available in the `charts/patroni-spilo/README.md` file within this repository.

## Install or update existing installation without using custom values file
This example is useful for automating deployment via GitHub actions when credentials can be specified via GitHub Secrets

```bash
helm upgrade --install dev-db ./charts/patroni-spilo \
             --set image.repository=registry.opensource.zalan.do/acid/spilo-12 \
             --set image.tag=1.6-p5 \
             --set credentials.random=true \
             --set persistentVolume.size=8G \
             --set persistentVolume.accessModes=["ReadWriteMany"] \
             --set resources.limits.cpu=100m \
             --set resources.limits.memory=128Mi \
             --set resources.requests.cpu=50m \
             --set resources.requests.memory=50Mi \
             --set replicaCount=3
```
Notes:

- `helm upgrade --install` instructs Helm to install release if it does not already exist, or upgrades if it does exist.
- `dev-db` specifies the release name of the deployment.
- `./charts/patroni-spilo` tells Helm the location of the chart.
- `-f ./charts/patroni-spilo/dev-values.yaml` tells Helm to use custom values files.
- `--set ...` overwrites parameters defined in `values.yaml` file

---

## Install or update existing installation with a custom values file
- Create a `dev-values.yaml` (for example) file
- Copy parameters you need to alter (from default) from `values.yaml` and modify them as necessary
- NOTE: Do not commit passwords

```bash
helm upgrade --install dev-db ./charts/patroni-spilo -f ./charts/patroni-spilo/dev-values.yaml
```

Notes:

- `helm upgrade --install` instructs Helm to install release if it does not already exist, or upgrades if it does exist.
- `dev-db` specifies the release name of the deployment.
- `./charts/patroni-spilo` tells Helm the location of the chart.
- `-f ./charts/patroni-spilo/dev-values.yaml` tells Helm to use custom values files.


## Potential edge cases when upgrading existing deployments with Helm
Some Kubernetes objects or parameters within those objects are imutable after creation. Please test upgrade procedures in DEV environment before completing in Production environments. The following are some common edge cases to be prepared for when installing/upgrading applications with Helm.

Secrets:
- When `credentials.random` is set to `true` there is potential for passwords to get updated. Other deployments depending on Postgres deployment may lose connectivity with PostgreSQL server if pods are not restarted following a `helm upgrade`.