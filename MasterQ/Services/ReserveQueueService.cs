﻿using System;
using Newtonsoft.Json.Linq;

namespace MasterQ
{
    public class ReserveQueueService
    {
		private static ReserveQueueService instance = new ReserveQueueService();

		ReserveQueueService()
		{ }
		public static ReserveQueueService getInstance()
		{
			return instance;
		}
        public ReserveQueueRs CallReserveQueue(Service input)
		{
			string serviceUrl = ServiceURL.ipServer + ServiceURL.reserveQueueUrl;

			ReserveQueueRq postData = new ReserveQueueRq();
			postData.branchID = input.branchID;
            postData.serviceID = input.serviceID;

			String resJSON = CallServices.callPost(serviceUrl, postData);
			return JObject.Parse(resJSON).ToObject<ReserveQueueRs>();

		}
		public GetServicesRs CallGetBranchServices(Branch input)
		{
			string serviceUrl = ServiceURL.ipServer + ServiceURL.getBranchServicesUrl;

			GetServicesRq postData = new GetServicesRq();
			postData.branchID = input.branchID;

			String resJSON = CallServices.callPost(serviceUrl, postData);
			return JObject.Parse(resJSON).ToObject<GetServicesRs>();

		}
    }
}