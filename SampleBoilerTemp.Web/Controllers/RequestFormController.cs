using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SampleBoilerTemp.Web.Models.DAO;
using System.Web.Http.Description;
using System.Threading.Tasks;
using SampleBoilerTemp.Web.Models.Account;
using System.Web.Mvc;

namespace SampleBoilerTemp.Web.App_Start
{
    public class RequestFormController : Controller
    {
        private CoreRequestEntities smsEntity = new CoreRequestEntities();

        public ActionResult GetAllRequests()
        {
            var values = from request in smsEntity.StationaryRequests
                         join st in smsEntity.Status on request.StatusId equals st.StatusId
                         join user in smsEntity.AbpUsers on request.UserId equals user.Id
                         select new { request.RequestId, st.StatusDescription, user.UserName, request.CreatedDate };

            return Json(values.ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllParticulars()
        {
            var values = from particular in smsEntity.SMS_Particulars select new { particular.Item_id,particular.Item_Description};
            return Json(values.ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetUnits()
        {
            var values = from unit in smsEntity.SMS_Units select new { unit.Unit_Id, unit.Unit_Description };
            return Json(values.ToList(), JsonRequestBehavior.AllowGet);
        }

        public string PostStationaryRequestDetails(List<Stationary_Request> stRequest)
        {
            long userid = stRequest[0].UserId;
            int? tenantid = stRequest[0].TenantId;
            var username = (from user in smsEntity.AbpUsers where user.Id == userid && user.TenantId==tenantid  select user.UserName).FirstOrDefault();

            //var user = smsEntity.AbpUsers.Select(x => x.Id == stRequest[0].UserId).SingleOrDefault();
           // var userName = smsEntity.AbpUsers.Where(a => a.Id == stRequest[0].UserId).Select(x => x.UserName).SingleOrDefault();
            //var userName = b.Countries.First(a => a.DOTWInternalID == citee.CountryCode).ID from test in smsEntity.AbpUsers where test.Id == stRequest.UserId && test.TenantId == stRequest.TenantId select test.UserName.FirstOrDefault();
            StationaryRequest stationaryRequest = new StationaryRequest();
            stationaryRequest.UserId = stRequest[0].UserId;
            stationaryRequest.TenantId = stRequest[0].TenantId;
            stationaryRequest.CreatedBy = username;//userName.ToString();
            stationaryRequest.StatusId = 1;
            stationaryRequest.IsActive = 1;
            stationaryRequest.CreatedDate = DateTime.Now;
            smsEntity.StationaryRequests.Add(stationaryRequest);
            smsEntity.SaveChanges();

            //var reqId = from test in smsEntity.StationaryRequests where test.UserId == stRequest.UserId && test.TenantId == stRequest.TenantId && test.RequestId == stationaryRequest.RequestId select test.RequestId.va
            //var reqId = smsEntity.StationaryRequests.First(a => a.UserId == stRequest.UserId && a.TenantId == stRequest.TenantId).RequestId;
            var reqIds = (from req in smsEntity.StationaryRequests where req.UserId == userid && req.TenantId == tenantid orderby req.RequestId descending select req.RequestId).FirstOrDefault();
            //var reqId = smsEntity.StationaryRequests.Where(a => a.UserId == stRequest[0].UserId && a.TenantId == stRequest[0].TenantId)
                    //      .OrderByDescending(a => a.RequestId)
                      //   .Select(a => a.RequestId).FirstOrDefault();

            foreach (Stationary_Request obj in stRequest)
            {


                StationaryRequestDetail stationaryRequestDetail = new StationaryRequestDetail();
                stationaryRequestDetail.ParticularId = obj.ParticularId;
                stationaryRequestDetail.RequestId = reqIds;
                stationaryRequestDetail.UnitId = obj.UnitId;
                stationaryRequestDetail.Rate = obj.Rate;
                stationaryRequestDetail.Vat = obj.Vat;
                stationaryRequestDetail.VateRate = stationaryRequestDetail.Rate * stationaryRequestDetail.Vat / 100; ;
                stationaryRequestDetail.FinalRate = stationaryRequestDetail.Rate + stationaryRequestDetail.VateRate;
                stationaryRequestDetail.Quantity = obj.Quantity;
                stationaryRequestDetail.Cost = stationaryRequestDetail.Quantity * stationaryRequestDetail.FinalRate;
                smsEntity.StationaryRequestDetails.Add(stationaryRequestDetail);
                smsEntity.SaveChanges();
            }


            return "";
        }
        public void Add()
        {
            //var val = smsEntity.AbpUsers.Select(x => x.Id == 1 && x.TenantId == 1).FirstOrDefault();
            var userName = from test in smsEntity.AbpUsers where test.Id == 1 select test.UserName.FirstOrDefault();
            StationaryRequest obj = new StationaryRequest();
            obj.UserId = 1;
            obj.TenantId = 1;
            obj.CreatedBy = userName.ToString();

            smsEntity.StationaryRequests.Add(obj);
            smsEntity.SaveChanges();

        }


    }
}
