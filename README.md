"# docker_ui" 

docker build -t docker_api .
docker run -d -p 83:80 --name docker_api --env HOST="host" --env LOGIN="login" --env PASSWORD="password" docker_api
