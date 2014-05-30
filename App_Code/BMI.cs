using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public class BMI
{
    public static long GetOrderID()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["BMICS"].ConnectionString);
        SqlCommand cmd = new SqlCommand("BMIOperations_GetOrderID", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@OrderID", SqlDbType.BigInt)).Direction = ParameterDirection.Output;

        try
        {
            con.Open();
            cmd.ExecuteScalar();
            return (long)cmd.Parameters["@OrderID"].Value;
        }
        catch
        {
            return 0;
        }
        finally
        {
            con.Close();
        }
    }

    public static int SaveOreder(long orderId, string refId, string resCode, long saleReferenceId)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["BMICS"].ConnectionString);
        SqlCommand cmd = new SqlCommand("BMIOperations_Save", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@OrderID", SqlDbType.BigInt)).Value = orderId;
        cmd.Parameters.Add(new SqlParameter("@RefId", SqlDbType.VarChar, 25)).Value = refId;
        cmd.Parameters.Add(new SqlParameter("@ResCode", SqlDbType.VarChar, 3)).Value = resCode;
        cmd.Parameters.Add(new SqlParameter("@SaleReferenceId", SqlDbType.BigInt)).Value = saleReferenceId;

        try
        {
            DataTable dtObj = new DataTable();
            con.Open();
            return cmd.ExecuteNonQuery();
        }
        catch
        {
            return 0;
        }
        finally
        {
            con.Close();
        }
    }

    public static long SettleOrder(long orderId)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["BMICS"].ConnectionString);
        SqlCommand cmd = new SqlCommand("BMIOperations_Settle", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@OrderID", SqlDbType.BigInt)).Value = orderId;

        try
        {
            con.Open();
            return cmd.ExecuteNonQuery();
        }
        catch
        {
            return 0;
        }
        finally
        {
            con.Close();
        }
    }

    public static void PreLoan(string accNo, ref int amount, ref string payDate, ref string name, ref byte outputState)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CoffCS"].ConnectionString);
        SqlCommand cmd = new SqlCommand("Web_BankGetLoanInfo", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@AccNo", SqlDbType.VarChar, 36)).Value = accNo;
        cmd.Parameters.Add(new SqlParameter("@PayAmount", SqlDbType.Money)).Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@InstAmount", SqlDbType.Money)).Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@PayDate", SqlDbType.VarChar, 8)).Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 91)).Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@Out_", SqlDbType.TinyInt)).Direction = ParameterDirection.Output;

        try
        {
            con.Open();
            cmd.ExecuteScalar();
            outputState = (byte)cmd.Parameters["@Out_"].Value;
            if (outputState == 0)
            {
                amount = Convert.ToInt32(cmd.Parameters["@InstAmount"].Value);
                payDate = (string)cmd.Parameters["@PayDate"].Value;
                name = (string)cmd.Parameters["@Name"].Value;
            }
        }
        catch
        {
            outputState = 1;
        }
        finally
        {
            con.Close();
        }
    }

    public static void PayLoan(string toAccNo, string amount, long followNo, ref byte outputState)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CoffCS"].ConnectionString);
        SqlCommand cmd = new SqlCommand("Web_BankTransLoanAmount", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@ToAccNo", SqlDbType.VarChar, 36)).Value = toAccNo;
        cmd.Parameters.Add(new SqlParameter("@Amount", SqlDbType.VarChar, 10)).Value = amount;
        cmd.Parameters.Add(new SqlParameter("@ToHName", SqlDbType.NVarChar, 50)).Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@FollowNo", SqlDbType.BigInt)).Value = followNo;
        cmd.Parameters.Add(new SqlParameter("@Out_", SqlDbType.TinyInt)).Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@Date", SqlDbType.VarChar, 10)).Direction = ParameterDirection.Output;

        try
        {
            con.Open();
            cmd.ExecuteScalar();
            outputState = (byte)cmd.Parameters["@Out_"].Value;
        }
        catch (Exception ex)
        {
            Public.Log(ex);
            outputState = 10;
        }
        finally
        {
            con.Close();
        }
    }

    public static void PreTransaction(string accNo, ref string name, ref string mobile, ref string hType, ref byte outputState)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CoffCS"].ConnectionString);
        SqlCommand cmd = new SqlCommand("Web_BankPassInfo", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@AccsNo", SqlDbType.VarChar, 36)).Value = accNo;
        cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 91)).Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@Mobile", SqlDbType.VarChar, 14)).Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@AccsName", SqlDbType.NVarChar, 60)).Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@Out", SqlDbType.TinyInt)).Direction = ParameterDirection.Output;

        try
        {
            con.Open();
            cmd.ExecuteScalar();
            outputState = (byte)cmd.Parameters["@Out"].Value;
            if (outputState == 0)
            {
                name = (string)cmd.Parameters["@Name"].Value;
                mobile = (string)cmd.Parameters["@Mobile"].Value;
                hType = (string)cmd.Parameters["@AccsName"].Value;
            }
        }
        catch
        {
            outputState = 1;
        }
        finally
        {
            con.Close();
        }
    }

    public static void TransferCash(string toAccNo, string amount, long followNo, ref byte outputState)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CoffCS"].ConnectionString);
        SqlCommand cmd = new SqlCommand("Web_BankTransAmount", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@ToAccNo", SqlDbType.VarChar, 36)).Value = toAccNo;
        cmd.Parameters.Add(new SqlParameter("@Amount", SqlDbType.VarChar, 10)).Value = amount;
        cmd.Parameters.Add(new SqlParameter("@ToHName", SqlDbType.NVarChar, 50)).Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@FollowNo", SqlDbType.BigInt)).Value = followNo;
        cmd.Parameters.Add(new SqlParameter("@Out_", SqlDbType.TinyInt)).Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@Date", SqlDbType.VarChar, 10)).Direction = ParameterDirection.Output;

        try
        {
            con.Open();
            cmd.ExecuteScalar();
            outputState = (byte)cmd.Parameters["@Out_"].Value;
        }
        catch
        {
            outputState = 3;
        }
        finally
        {
            con.Close();
        }
    }
}

public class TrnsactionInfo
{
    public string AccountNo { get; set; }
    public string Amount { get; set; }
}
