using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public class MarriageLoan
{
    public static DataTable GetCityList()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CoffCS"].ConnectionString);
        SqlCommand cmd = new SqlCommand("R_Cities", con);
        cmd.CommandType = CommandType.StoredProcedure;

        try
        {
            DataTable dtObj = new DataTable();
            con.Open();
            dtObj.Load(cmd.ExecuteReader());
            return dtObj;
        }
        catch
        {
            return null;
        }
        finally
        {
            con.Close();
        }
    }

    public static void SaveReq(string name, string family, string nN, string id, string father, string cb, string addr1, string addr2, string tel1, string tel2, string mobile, bool gender, string pOB, string bDate, string cMarr, string wName, string wFamily, string wNN, string wID, string wMobile, string wFather, int pers_Iden1, int pers_Iden2, string date, string bookNo, string bureauNo, ref byte _out, ref int counter, ref string refDate)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CoffCS"].ConnectionString);
        SqlCommand cmd = new SqlCommand("R_SaveReq", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 20)).Value = name;
        cmd.Parameters.Add(new SqlParameter("@Family", SqlDbType.NVarChar, 70)).Value = family;
        cmd.Parameters.Add(new SqlParameter("@NN", SqlDbType.VarChar, 10)).Value = nN;
        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.VarChar, 15)).Value = id;
        cmd.Parameters.Add(new SqlParameter("@Father", SqlDbType.NVarChar, 20)).Value = father;
        cmd.Parameters.Add(new SqlParameter("@CB", SqlDbType.NVarChar, 20)).Value = cb;
        cmd.Parameters.Add(new SqlParameter("@Addr1", SqlDbType.NVarChar, 100)).Value = addr1;
        cmd.Parameters.Add(new SqlParameter("@Addr2", SqlDbType.NVarChar, 100)).Value = addr2;
        cmd.Parameters.Add(new SqlParameter("@Tel1", SqlDbType.VarChar, 20)).Value = tel1;
        cmd.Parameters.Add(new SqlParameter("@Tel2", SqlDbType.VarChar, 20)).Value = tel2;
        cmd.Parameters.Add(new SqlParameter("@Mobile", SqlDbType.NVarChar, 14)).Value = mobile;
        cmd.Parameters.Add(new SqlParameter("@Gender", SqlDbType.Bit)).Value = gender;
        cmd.Parameters.Add(new SqlParameter("@POB", SqlDbType.VarChar, 10)).Value = pOB;
        cmd.Parameters.Add(new SqlParameter("@BDate", SqlDbType.VarChar, 8)).Value = bDate;
        cmd.Parameters.Add(new SqlParameter("@CMarr", SqlDbType.NVarChar, 20)).Value = cMarr;
        cmd.Parameters.Add(new SqlParameter("@WName", SqlDbType.NVarChar, 20)).Value = wName;
        cmd.Parameters.Add(new SqlParameter("@WFamily", SqlDbType.NVarChar, 70)).Value = wFamily;
        cmd.Parameters.Add(new SqlParameter("@WNN", SqlDbType.NVarChar, 10)).Value = wNN;
        cmd.Parameters.Add(new SqlParameter("@WID", SqlDbType.VarChar, 15)).Value = wID;
        cmd.Parameters.Add(new SqlParameter("@WMobile", SqlDbType.NVarChar, 14)).Value = mobile;
        cmd.Parameters.Add(new SqlParameter("@WFather", SqlDbType.NVarChar, 20)).Value = wFather;
        //cmd.Parameters.Add(new SqlParameter("@Pers_Iden1", SqlDbType.Int)).Value = pers_Iden1;
        //cmd.Parameters.Add(new SqlParameter("@Pers_Iden2", SqlDbType.Int)).Value = pers_Iden2;
        cmd.Parameters.Add(new SqlParameter("@Date", SqlDbType.VarChar, 8)).Value = date;
        cmd.Parameters.Add(new SqlParameter("@BookNo", SqlDbType.VarChar, 20)).Value = bookNo;
        cmd.Parameters.Add(new SqlParameter("@BureauNo", SqlDbType.VarChar, 15)).Value = bureauNo;
        cmd.Parameters.Add(new SqlParameter("@OUT", SqlDbType.TinyInt)).Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@Counter", SqlDbType.Int)).Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@RefDate", SqlDbType.VarChar, 8)).Direction = ParameterDirection.Output;

        try
        {
            con.Open();
            cmd.ExecuteScalar();
            _out = Convert.ToByte(cmd.Parameters["@Out"].Value);
            counter = Convert.ToInt32(cmd.Parameters["@Counter"].Value);
            refDate = (string)cmd.Parameters["@RefDate"].Value;
        }
        catch
        {

        }
        finally
        {
            con.Close();
        }
    }
}