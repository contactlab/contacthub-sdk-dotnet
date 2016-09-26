using System.Collections.Generic;

namespace ContactHubSdkLibrary.Models
{

	public enum QueryBuilderOperatorEnum
	{
		EQUALS,
		NOT_EQUALS,
		BETWEEN,
		GTE,
		GT,
		LTE,
		LT,
		IS_NULL,
		IS_NOT_NULL,
		IN,
		NOT_IN
	}

	public enum QueryBuilderConjunctionEnum
	{
		AND,
		OR
	}


	/*
	 *string querySTR = @"
								{
   
									""name"": ""no name"",
									""query"": {
												  ""type"": ""combined"",
												  ""name"": ""no name"",
												  ""conjunction"" : ""INTERSECT"",
												  ""queries"" : 
													  [
													  {
														""are"": {
																""condition"": {
																""attribute"": ""base.firstName"",
																""operator"": ""EQUALS"",
																""type"": ""atomic"",
																""value"": ""Diego""
																				}
																 },
														""name"": ""No name"",
														""type"": ""simple""
														}	,												  
													  {
														""are"": {
																""condition"": {
																""attribute"": ""base.lastName"",
																""operator"": ""EQUALS"",
																""type"": ""atomic"",
																""value"": ""Feltrin""
																				}
																 },
														""name"": ""No name"",
														""type"": ""simple""
														}													  
													  ]
									}
								}";

	*/

	public class QueryBuilderItem
	{
		public string attributeName { get; set; }

		public string attributeValue { get; set; }

		public QueryBuilderOperatorEnum attributeOperator { get; set; }
	}


	public class QueryBuilder
	{
		List<QueryBuilderItem> queries = null;


		public QueryBuilder()
		{
			//init
			queries = new List<QueryBuilderItem>();
		}

		public void AddQuery(QueryBuilderItem item)
		{
			queries.Add(item);
		}

		public string GenerateQuery(QueryBuilderConjunctionEnum conjunction)
		{
			string conjunctionSTR = (conjunction == QueryBuilderConjunctionEnum.AND ? "INTERSECT" : "UNION");
			string querySTR = @"{
									""name"": ""no name"",
									""query"": {
												  ""type"": ""combined"",
												  ""name"": ""no name"",
												  ""conjunction"" : """;
			querySTR += conjunctionSTR + @""",
												  ""queries"" : 
													  [";

			foreach (QueryBuilderItem q in queries)
			{
				querySTR += "  {";
				querySTR += @"""are"": { ";
				querySTR += @" ""condition"": {";
				querySTR += string.Format("\"attribute\": \"{0}\",", q.attributeName);
				querySTR += string.Format("\"operator\": \"{0}\",", q.attributeOperator.ToString());
				querySTR += @" ""type"": ""atomic"",";
				querySTR += string.Format("\"value\": \"{0}\"", q.attributeValue);
				querySTR += @"
												}
										 },
														""name"": ""No name"",
														""type"": ""simple""
														}
                        ";
                querySTR += ",";
			}

            if (querySTR.EndsWith(","))
            {
                querySTR = querySTR.Substring(0, querySTR.Length - 1);
            }
			querySTR += @" ]
									}
						}";
			return querySTR;
		}
	}

}
