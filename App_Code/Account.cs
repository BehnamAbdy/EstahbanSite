using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public class Account
{
    public static void Balance(string accsNo, string pass, ref string name, ref string rem1, ref string rem2, ref string hType, ref byte outputState)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CoffCS"].ConnectionString);
        SqlCommand cmd = new SqlCommand("Web_GetRemainder", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@AccsNo", SqlDbType.VarChar, 36)).Value = accsNo;
        cmd.Parameters.Add(new SqlParameter("@Pass", SqlDbType.VarChar, 256)).Value = pass;
        cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar, 150)).Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@Mobile", SqlDbType.VarChar, 14)).Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@Rem1", SqlDbType.Money)).Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@Rem2", SqlDbType.Money)).Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@HName", SqlDbType.NVarChar, 50)).Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@OutputState", SqlDbType.TinyInt)).Direction = ParameterDirection.Output;

        try
        {
            con.Open();
            cmd.ExecuteScalar();
            outputState = (byte)cmd.Parameters["@OutputState"].Value;
            if (outputState == 0)
            {
                name = (string)cmd.Parameters["@Name"].Value;
                rem1 = Convert.ToInt32(cmd.Parameters["@Rem1"].Value).ToString("#,##0");
                rem2 = Convert.ToInt32(cmd.Parameters["@Rem2"].Value).ToString("#,##0");
                hType = (string)cmd.Parameters["@HName"].Value;
            }
        }
        catch
        {
            outputState = 10;
        }
        finally
        {
            con.Close();
        }
    }

    public static DataTable Transactions(string accsNo, string pass, string frDate, string toDate, ref string name, ref string hType, ref string amount1, ref string amount2, ref byte outputState)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CoffCS"].ConnectionString);
        SqlCommand cmd = new SqlCommand("Web_BrowCard", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@AccNo", SqlDbType.VarChar, 36)).Value = accsNo;
        cmd.Parameters.Add(new SqlParameter("@Pass", SqlDbType.VarChar, 256)).Value = pass;
        cmd.Parameters.Add(new SqlParameter("@FrDate", SqlDbType.VarChar, 8)).Value = frDate;
        cmd.Parameters.Add(new SqlParameter("@ToDate", SqlDbType.VarChar, 8)).Value = toDate;
        cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 91)).Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@HType", SqlDbType.NVarChar, 50)).Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@Mobile", SqlDbType.VarChar, 14)).Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@Amount1", SqlDbType.Money)).Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@Amount2", SqlDbType.Money)).Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@Out_", SqlDbType.TinyInt)).Direction = ParameterDirection.Output;

        try
        {
            DataTable dtObj = new DataTable();
            con.Open();
            dtObj.Load(cmd.ExecuteReader());
            outputState = (byte)cmd.Parameters["@Out_"].Value;
            if (outputState == 0)
            {
                name = (string)cmd.Parameters["@Name"].Value;
                hType = (string)cmd.Parameters["@HType"].Value;
                amount1 = Convert.ToInt32(cmd.Parameters["@Amount1"].Value).ToString("#,##0");
                amount2 = Convert.ToInt32(cmd.Parameters["@Amount2"].Value).ToString("#,##0");
            }
            return dtObj;
        }
        catch
        {
            outputState = 10;
            return null;
        }
        finally
        {
            con.Close();
        }
    }

    public static void PreTransaction(string fromAccNo, string toAccNo, string pass, ref string fromName, ref string fromHName, ref string toName, ref string toHName, ref byte outputState)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CoffCS"].ConnectionString);
        SqlCommand cmd = new SqlCommand("Web_PreTrans", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@FromAccNo", SqlDbType.VarChar, 36)).Value = fromAccNo;
        cmd.Parameters.Add(new SqlParameter("@ToAccNo", SqlDbType.VarChar, 36)).Value = toAccNo;
        cmd.Parameters.Add(new SqlParameter("@Pass", SqlDbType.VarChar, 256)).Value = pass;
        cmd.Parameters.Add(new SqlParameter("@FromName", SqlDbType.NVarChar, 91)).Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@FromMobile", SqlDbType.NVarChar, 14)).Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@FromHName", SqlDbType.NVarChar, 50)).Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@ToName", SqlDbType.NVarChar, 91)).Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@ToMobile", SqlDbType.NVarChar, 14)).Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@ToHName", SqlDbType.NVarChar, 50)).Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@Out_", SqlDbType.TinyInt)).Direction = ParameterDirection.Output;

        try
        {
            con.Open();
            cmd.ExecuteScalar();
            outputState = (byte)cmd.Parameters["@Out_"].Value;
            if (outputState == 0)
            {
                fromName = (string)cmd.Parameters["@FromName"].Value;
                fromHName = (string)cmd.Parameters["@FromHName"].Value;
                toName = (string)cmd.Parameters["@ToName"].Value;
                toHName = (string)cmd.Parameters["@ToHName"].Value;
            }
        }
        catch
        {
            outputState = 10;
        }
        finally
        {
            con.Close();
        }
    }

    public static void TransferCash(string fromAccNo, string toAccNo, string pass, int amount, ref int followNo, ref byte outputState)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CoffCS"].ConnectionString);
        SqlCommand cmd = new SqlCommand("Web_TransAmount", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@FromAccNo", SqlDbType.VarChar, 36)).Value = fromAccNo;
        cmd.Parameters.Add(new SqlParameter("@ToAccNo", SqlDbType.VarChar, 36)).Value = toAccNo;
        cmd.Parameters.Add(new SqlParameter("@Amount", SqlDbType.Money)).Value = amount;
        cmd.Parameters.Add(new SqlParameter("@Pass", SqlDbType.VarChar, 256)).Value = pass;
        cmd.Parameters.Add(new SqlParameter("@FromHName", SqlDbType.NVarChar, 50)).Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@ToHName", SqlDbType.NVarChar, 50)).Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@FollowNo", SqlDbType.Int)).Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@Out_", SqlDbType.TinyInt)).Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@Date", SqlDbType.VarChar, 10)).Direction = ParameterDirection.Output;

        try
        {
            con.Open();
            cmd.ExecuteScalar();
            outputState = (byte)cmd.Parameters["@Out_"].Value;
            if (outputState == 0)
            {
                followNo = (int)cmd.Parameters["@FollowNo"].Value;
            }
        }
        catch
        {
            outputState = 10;
        }
        finally
        {
            con.Close();
        }
    }

    public static void PreLoan(string fromAccNo, string toAccNo, string pass, ref string fromName, ref string fromHName, ref string toName, ref string toHName, ref string amount, ref byte outputState)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CoffCS"].ConnectionString);
        SqlCommand cmd = new SqlCommand("Web_PreLoanTrans", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@FromAccNo", SqlDbType.VarChar, 36)).Value = fromAccNo;
        cmd.Parameters.Add(new SqlParameter("@ToAccNo", SqlDbType.VarChar, 36)).Value = toAccNo;
        cmd.Parameters.Add(new SqlParameter("@Pass", SqlDbType.VarChar, 256)).Value = pass;
        cmd.Parameters.Add(new SqlParameter("@FromName", SqlDbType.NVarChar, 91)).Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@FromMobile", SqlDbType.NVarChar, 14)).Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@FromHName", SqlDbType.NVarChar, 50)).Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@ToName", SqlDbType.NVarChar, 91)).Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@ToMobile", SqlDbType.NVarChar, 14)).Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@ToHName", SqlDbType.NVarChar, 50)).Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@Amount", SqlDbType.Money)).Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@Out_", SqlDbType.TinyInt)).Direction = ParameterDirection.Output;

        try
        {
            con.Open();
            cmd.ExecuteScalar();
            outputState = (byte)cmd.Parameters["@Out_"].Value;
            if (outputState == 0)
            {
                fromName = (string)cmd.Parameters["@FromName"].Value;
                fromHName = (string)cmd.Parameters["@FromHName"].Value;
                toName = (string)cmd.Parameters["@ToName"].Value;
                toHName = (string)cmd.Parameters["@ToHName"].Value;
                amount = Convert.ToInt32(cmd.Parameters["@Amount"].Value).ToString("#,##0");
            }
        }
        catch
        {
            outputState = 10;
        }
        finally
        {
            con.Close();
        }
    }

    public static void PayLoan(string fromAccNo, string toAccNo, string pass, decimal amount, ref int followNo, ref byte outputState)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CoffCS"].ConnectionString);
        SqlCommand cmd = new SqlCommand("Web_TransLoanAmount", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@FromAccNo", SqlDbType.VarChar, 36)).Value = fromAccNo;
        cmd.Parameters.Add(new SqlParameter("@ToAccNo", SqlDbType.VarChar, 36)).Value = toAccNo;
        cmd.Parameters.Add(new SqlParameter("@Pass", SqlDbType.VarChar, 256)).Value = pass;
        cmd.Parameters.Add(new SqlParameter("@Amount", SqlDbType.Money)).Value = amount;
        cmd.Parameters.Add(new SqlParameter("@FromHName", SqlDbType.NVarChar, 50)).Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@ToHName", SqlDbType.NVarChar, 50)).Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@FollowNo", SqlDbType.Int)).Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@Out_", SqlDbType.TinyInt)).Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@Date", SqlDbType.VarChar, 10)).Direction = ParameterDirection.Output;

        try
        {
            con.Open();
            cmd.ExecuteScalar();
            outputState = (byte)cmd.Parameters["@Out_"].Value;
            if (outputState == 0)
            {
                followNo = (int)cmd.Parameters["@FollowNo"].Value;
            }
        }
        catch
        {
            outputState = 10;
        }
        finally
        {
            con.Close();
        }
    }

    public static void ReceiveSms(string mobileNumber, string receiveDate, string receiveTime, string message)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CoffCS"].ConnectionString);
        SqlCommand cmd = new SqlCommand("SMS_Recieve", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@Recieve_CallNo", SqlDbType.VarChar, 14)).Value = mobileNumber;
        cmd.Parameters.Add(new SqlParameter("@Recieve_Date", SqlDbType.VarChar, 8)).Value = receiveDate;
        cmd.Parameters.Add(new SqlParameter("@Recieve_Time", SqlDbType.Char, 4)).Value = receiveTime;
        cmd.Parameters.Add(new SqlParameter("@Recieve_Message", SqlDbType.NVarChar, 160)).Value = message;

        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
        }
        catch
        { }
        finally
        {
            con.Close();
        }
    }

    public static DataTable LoanRecords(string accsNo, string pass, ref string amount, ref string payDate, ref string name, ref byte outputState)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CoffCS"].ConnectionString);
        SqlCommand cmd = new SqlCommand("Web_GetLoanInfo", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@AccNo", SqlDbType.VarChar, 36)).Value = accsNo;
        cmd.Parameters.Add(new SqlParameter("@Pass", SqlDbType.VarChar, 256)).Value = pass;
        cmd.Parameters.Add(new SqlParameter("@Amount", SqlDbType.Money)).Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@PayDate", SqlDbType.VarChar, 8)).Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 91)).Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@Out_", SqlDbType.TinyInt)).Direction = ParameterDirection.Output;

        try
        {
            DataTable dtObj = new DataTable();
            con.Open();
            dtObj.Load(cmd.ExecuteReader());
            outputState = (byte)cmd.Parameters["@Out_"].Value;
            if (outputState == 0)
            {
                amount = Convert.ToInt32(cmd.Parameters["@Amount"].Value).ToString("#,##0");
                payDate = (string)cmd.Parameters["@PayDate"].Value;
                name = cmd.Parameters["@Name"].Value == DBNull.Value ? string.Empty : (string)cmd.Parameters["@Name"].Value;
                outputState = (byte)cmd.Parameters["@Out_"].Value;
            }
            return dtObj;
        }
        catch
        {
            outputState = 10;
            return null;
        }
        finally
        {
            con.Close();
        }
    }

    public static void PreEditPassword(string accNo, string pass, ref string name, ref string hType, ref string mobile, ref byte outputState)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CoffCS"].ConnectionString);
        SqlCommand cmd = new SqlCommand("Web_PassInfo", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@AccsNo", SqlDbType.VarChar, 36)).Value = accNo;
        cmd.Parameters.Add(new SqlParameter("@Pass", SqlDbType.VarChar, 256)).Value = pass;
        cmd.Parameters.Add(new SqlParameter("@HType", SqlDbType.NVarChar, 50)).Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 91)).Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@Mobile", SqlDbType.VarChar, 14)).Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@Out", SqlDbType.TinyInt)).Direction = ParameterDirection.Output;

        try
        {
            con.Open();
            cmd.ExecuteScalar();
            outputState = (byte)cmd.Parameters["@Out"].Value;
            if (outputState == 0)
            {
                name = (string)cmd.Parameters["@Name"].Value;
                hType = (string)cmd.Parameters["@HType"].Value;
                mobile = (string)cmd.Parameters["@Mobile"].Value;
            }
        }
        catch
        {
            outputState = 10;
        }
        finally
        {
            con.Close();
        }
    }

    public static void EditPassword(string accNo, string pass, ref byte outputState)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CoffCS"].ConnectionString);
        SqlCommand cmd = new SqlCommand("Web_ChangePass", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@AccsNo", SqlDbType.VarChar, 36)).Value = accNo;
        cmd.Parameters.Add(new SqlParameter("@Pass", SqlDbType.VarChar, 256)).Value = pass;
        cmd.Parameters.Add(new SqlParameter("@Out", SqlDbType.TinyInt)).Direction = ParameterDirection.Output;

        try
        {
            con.Open();
            cmd.ExecuteScalar();
            outputState = (byte)cmd.Parameters["@Out"].Value;
        }
        catch
        {
            outputState = 10;
        }
        finally
        {
            con.Close();
        }
    }
}
