using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Renci.SshNet;

namespace Docker.Controllers
{
    [Route("")]
    [ApiController]
    public class DockerController : ControllerBase
    {
        private string _hostName;
        private string _login;
        private string _password;
        public DockerController()
        {
            _hostName = Environment.GetEnvironmentVariable("HOST");
            _login = Environment.GetEnvironmentVariable("LOGIN");
            _password = Environment.GetEnvironmentVariable("PASSWORD");
        }
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            string result = "";
            using (var client = new SshClient(_hostName, _login, _password))
            {
                client.Connect();
                var s = client.RunCommand("docker ps --format \"{{.Names}}\"");
                result = s.Result;
                client.Disconnect();
            }
            return result.Split(Environment.NewLine).Where(c => !string.IsNullOrEmpty(c)).ToList();
        }

        [HttpGet("Logs/{name}")]
        public ActionResult<string> Logs(string name)
        {
            string result = "";
            using (var client = new SshClient(_hostName, _login, _password))
            {
                client.Connect();
                var s = client.RunCommand("docker logs " + name);
                result = s.Result;
                client.Disconnect();
            }
            return result;
        }
        [HttpGet("Start/{name}")]
        public ActionResult<string> Start(string name)
        {
            string result = "";
            using (var client = new SshClient(_hostName, _login, _password))
            {
                client.Connect();
                var s = client.RunCommand("docker start " + name);
                result = s.Result;
                client.Disconnect();
            }
            return result;
        }
        [HttpGet("Stop/{name}")]
        public ActionResult<string> Stop(string name)
        {
            string result = "";
            using (var client = new SshClient(_hostName, _login, _password))
            {
                client.Connect();
                var s = client.RunCommand("docker stop " + name);
                result = s.Result;
                client.Disconnect();
            }
            return result;
        }
    }
}
