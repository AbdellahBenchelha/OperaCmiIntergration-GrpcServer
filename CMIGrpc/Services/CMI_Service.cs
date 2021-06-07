using CMIGrpc.Data;
using CMIGrpc.Protos;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMIGrpc.Services
{
    public class CMI_Service : CMIProtoService.CMIProtoServiceBase
    {
        private readonly ILogger<CMI_Service> _logger;

        public CMI_Service(ILogger<CMI_Service> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override async Task<PaymentModel> Payment(PaymentRequest request, ServerCallContext context)
        {
            var receivedData = Socket_Connection.Payment(request.TAGAMOUNT, request.TAGCURRENCY);
            
            if (receivedData==null)
            {
                throw new ArgumentNullException();
            }

            var payment = new PaymentModel();
            payment.TAGAMOUNT = "0000";
            payment.TAGEFTSTAN = "111";
            payment.TAGCARDNUMMASK = SplitSocketData.GetValueKey(receivedData,"001B");



            return payment;
        }
        public override async Task<PaymentModel> preautorisation(PaymentRequest request, ServerCallContext context)
        {
            var receivedData = Socket_Connection.preauthorisation(request.TAGAMOUNT, request.TAGCURRENCY);

            if (receivedData == null)
            {
                throw new ArgumentNullException();
            }

            var payment = new PaymentModel();
            payment.TAGAMOUNT = "0000";
            payment.TAGEFTSTAN = "111";
            payment.TAGCARDNUMMASK = SplitSocketData.GetValueKey(receivedData, "001B");



            return payment;
        }
      
        
        public async override Task<PaymentModel> preautorisatio_avoid(PaymentRequest request, ServerCallContext context)
        {
            var receivedData = Socket_Connection.preauthorisation_avoid(request.TAGAMOUNT, request.TAGCURRENCY);

            if (receivedData == null)
            {
                throw new ArgumentNullException();
            }

            var payment = new PaymentModel();
            payment.TAGAMOUNT = "0000";
            payment.TAGEFTSTAN = "111";
            payment.TAGCARDNUMMASK = "TT";



            return payment;
        }


        public async override Task<PaymentModel> preautorisatio_confirmation(PaymentRequest request, ServerCallContext context)
        {
            var receivedData = Socket_Connection.preauthorisation_confirmayion(request.TAGAMOUNT, request.TAGCURRENCY);

            if (receivedData == null)
            {
                throw new ArgumentNullException();
            }

            var payment = new PaymentModel();
            payment.TAGAMOUNT = "0000";
            payment.TAGEFTSTAN = "111";
            payment.TAGCARDNUMMASK = SplitSocketData.GetValueKey(receivedData, "001B");



            return payment;
        }


    }
}
