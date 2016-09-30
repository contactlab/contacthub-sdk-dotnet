using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ContactHubSdkLibrary.SDKclasses
{
	 public class Workspace
	{
        private List<String> _nodes;
        private string _id;
        private string _token;
        public Boolean isValid = false;
        public List<String> nodes
        {
            get { return _nodes; }
        }
        public string id
        {
            get { return _id; }
        }
        public string token
        {
            get { return _token; }
        }

        public Workspace(string workspaceID, string token)
		{
			 _nodes = new List<String>();
            _id = workspaceID;

            /* initialize workspace: get node list from configuration (/me) */

            /* TO BE DONE: get node list */
            _nodes.Add("d35a5485-ff59-4b85-bbc3-1eb45ed9bcd6");

        }
		public Node GetNode(string nodeID)
        {
            /* TO BE DONE: verify if nodeID is in node list */
            if (!_nodes.Contains(nodeID)) return null;

            Node returnValue = new Node(_id, _token, nodeID);
            return returnValue;
        }
	}
}
