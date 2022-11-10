```
docker run \
  --rm \
  --publish 80:80/tcp \
  ghcr.io/oscarhult/ytxpl
```

```
docker run \
  --rm \
  --env ASPNETCORE_URLS="http://+:5555" \
  --publish 5555:5555/tcp \
  ghcr.io/oscarhult/ytxpl
```

```
docker run \
  --rm \
  --read-only \
  --env TZ="Europe/Stockholm" \
  --env DOTNET_EnableDiagnostics="0" \
  --env ASPNETCORE_URLS="http://+:5555" \
  --publish 5555:5555/tcp \
  ghcr.io/oscarhult/ytxpl
```
