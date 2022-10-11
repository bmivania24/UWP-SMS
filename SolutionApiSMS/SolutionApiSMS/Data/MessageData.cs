using ApiSMS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ApiSMS.Data
{
    public class MessageData
    {
        public  object  InsertMsg(MessageModel message)
        {
            try
            {
                object  resultado;
                Conection  cn = new Conection ();

                using (SqlCommand cmd = new SqlCommand("sp_Messages", cn.OpenConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@option", 1);
                    cmd.Parameters.AddWithValue("@ToNumber", message.ToNumber );
                    cmd.Parameters.AddWithValue("@FromNumber", message.FromNumber);
                    cmd.Parameters.AddWithValue("@Message", message.Message );
                    resultado = cmd.ExecuteScalar ();
                    cn.CloseConnection();
                }


                return resultado;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MessageModel > list()
        {
            try
            {
                List<MessageModel> MsgList = new List<MessageModel>();

                MessageModel objMsg = null;

                Conection cn = new Conection();
                using (SqlCommand cmd = new SqlCommand("sp_Messages", cn.OpenConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@option", 2);

                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            objMsg = new MessageModel ();
                            objMsg.idMessage  = Convert.ToInt32(dataReader["IdMessage"]);
                            objMsg.FromNumber = Convert.ToString(dataReader["FromNumber"]);
                            objMsg.ToNumber = Convert.ToString(dataReader["ToNumber"]);
                            objMsg.dateCreated = (Convert.ToDateTime(dataReader["DateCreated"])).ToString("dd/MM/yyyy");
                            objMsg.Message = Convert.ToString(dataReader["Message"]);

                            MsgList.Add(objMsg);
                        }
                    }
                  
                    cn.CloseConnection();
                }
              

                return MsgList;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public  int InsertMsgResoponse(MsgResponseTwModel  messageResponse)
        {
            try
            {
                int resultado;
                Conection cn = new Conection();

                using (SqlCommand cmd = new SqlCommand("sp_ResponseMessages", cn.OpenConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@option", 1);
                    cmd.Parameters.AddWithValue("@ConfirmationCode", messageResponse.confirmationCode );
                    cmd.Parameters.AddWithValue("@IdMessage", messageResponse.IdMessage);
                  
                    resultado = cmd.ExecuteNonQuery();
                    cn.CloseConnection();
                }


                return resultado;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MsgResponseTwModel> listResponses()
        {
            try
            {
                List<MsgResponseTwModel> MsgList = new List<MsgResponseTwModel>();

                MsgResponseTwModel objResponse = null;

                Conection cn = new Conection();
                using (SqlCommand cmd = new SqlCommand("sp_ResponseMessages", cn.OpenConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@option", 2);

                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            objResponse = new MsgResponseTwModel();
                            objResponse.IdMessageResponseTw  = Convert.ToInt32(dataReader["IdResponsesMessageTw"]);
                            objResponse.IdMessage  = Convert.ToInt32(dataReader["IdMessage"]);
                            objResponse.ToNumber = Convert.ToString(dataReader["ToNumber"]);
                            objResponse.FromNumber = Convert.ToString(dataReader["FromNumber"]);
                            objResponse.dateCreated  = (Convert.ToDateTime(dataReader["DateCreated"])).ToString("dd/MM/yyyy");
                            objResponse.DateSent  = (Convert.ToDateTime(dataReader["DateSent"])).ToString("dd/MM/yyyy");
                            objResponse.Message = Convert.ToString(dataReader["Message"]);
                            objResponse.confirmationCode  = Convert.ToString(dataReader["ConfirmationCode"]);
                            MsgList.Add(objResponse);
                        }
                    }

                    cn.CloseConnection();
                }


                return MsgList;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}