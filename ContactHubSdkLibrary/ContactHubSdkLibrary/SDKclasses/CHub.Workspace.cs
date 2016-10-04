using System;

namespace ContactHubSdkLibrary.SDKclasses
{
    public class Workspace
    {
        //		private List<String> _nodes;    // future implementation
        private string _id;
        private string _token;
        public Boolean isValid
        { get { return _isValid; } }
        private Boolean _isValid;

        //public List<String> nodes // future implementation
        //{
        //	get { return _nodes; }
        //}
        public string id
        {
            get { return _id; }
        }
        public string token
        {
            get { return _token; }
        }

        public Workspace(string workspaceID, string tokenID)
        {
            // _nodes = new List<String>();
            _id = workspaceID;
            _token = tokenID;
            /* initialize workspace: get node list from configuration (/me): this is future implementation */
            _isValid = true;
            /* TO BE DONE: get node list. This is future implementation */
        }
        public Node GetNode(string nodeID)
        {
            /* TO BE DONE: verify if nodeID is in node list */
            //if (!_nodes.Contains(nodeID)) return null;

            Node returnValue = new Node(_id, _token, nodeID);
            return returnValue;
        }
    }
}
