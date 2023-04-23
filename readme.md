# Metrics

Launch infrastructure:

```bash
make run-infrastructure
```

Run `Metrics.Api` project in IDE and navigate to:

- Swagger UI at <http://localhost:6050>.
- Metrics page at <http://localhost:6050/metrics>


## Prometheus

Prometheus UI: <http://localhost:6070>.

Check targets at <http://localhost:6070/targets>

## Grafana

Grafana UI: <http://localhost:6080>. 
 
Use <http://host.docker.internal:6070> as Prometheus' datasource URL.

Import dashboards from JSON files inside `grafana` folder.
