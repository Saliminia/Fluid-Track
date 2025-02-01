using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Data;

namespace DMR
{
    public class ProcedureParam
    {
        public enum ParamType
        {
            PT_String,
            PT_Decimal,
            PT_Int,
            PT_BigInt,
            PT_Image,
            PT_Bit,
            PT_DateTime,
            PT_Structured
        }
        //=======================================================
        public string varNameInSqlProc = "@var";
        public object paramValue = null;
        public ParamType type = ParamType.PT_String;
        public bool hasMinCondition = false;
        public string minValue = "";
        public bool hasMaxCondition = false;
        public string maxValue = "";
        public string invalidityError = "Invalid Parameter";
        //=======================================================
        public ProcedureParam(string varNameInSqlProc, string value, ParamType type, bool hasMinCondition,
            string minValue, bool hasMaxCondition, string maxValue, string invalidityError)
        {
            this.varNameInSqlProc = varNameInSqlProc;
            this.paramValue = value;
            this.type = type;
            this.hasMinCondition = hasMinCondition;
            this.minValue = minValue;
            this.hasMaxCondition = hasMaxCondition;
            this.maxValue = maxValue;
            this.invalidityError = invalidityError;
        }
        //-------------------------------------------------------
        public ProcedureParam(string varNameInSqlProc, string value, int maxLen)//String
        {
            this.varNameInSqlProc = varNameInSqlProc;

            if (value.Length <= maxLen)
                this.paramValue = value;
            else
                this.paramValue = value.Substring(0, maxLen);

            this.type = ParamType.PT_String;
        }
        //-------------------------------------------------------
        public ProcedureParam(string varNameInSqlProc, Image img)//Image
        {
            this.varNameInSqlProc = varNameInSqlProc;
            this.paramValue = img;
            this.type = ParamType.PT_Image;
        }
        //-------------------------------------------------------
        public ProcedureParam(string varNameInSqlProc, DateTime dt)//DateTime
        {
            this.varNameInSqlProc = varNameInSqlProc;
            this.paramValue = dt;
            this.type = ParamType.PT_DateTime;
        }
        //-------------------------------------------------------
        public ProcedureParam(string varNameInSqlProc, DataTable dt)//Structured
        {
            this.varNameInSqlProc = varNameInSqlProc;
            this.paramValue = dt;
            this.type = ParamType.PT_Structured;
        }
        //-------------------------------------------------------
        public ProcedureParam(string varNameInSqlProc, ParamType type)//Null
        {
            this.varNameInSqlProc = varNameInSqlProc;
            this.paramValue = null;
            this.type = type;
        }
        //-------------------------------------------------------
        public SqlParameter GenerateSqlParameter(out string errString)
        {
            SqlParameter result = null;


            switch (type)
            {
                //******************************************
                case ParamType.PT_Int:
                    {
                        if (paramValue == null)
                        {
                            result = new SqlParameter(varNameInSqlProc, System.Data.SqlDbType.Int);
                            result.Value = DBNull.Value;
                            break;
                        }

                        string value = paramValue.ToString().Trim();

                        int intVal = -1;

                        if (!AdvancedConvertor.ToInt(value, ref intVal))
                        {
                            errString = invalidityError + ": " + value;
                            return null;
                        }
                        //--------------------------
                        if (hasMinCondition)
                        {
                            int min = -1;

                            if (!AdvancedConvertor.ToInt(minValue.Trim(), ref min))
                            {
                                errString = "Invalid Minimum: " + min.ToString();
                                return null;
                            }

                            if (!(intVal >= min))
                            {
                                errString = invalidityError + ": " + value;
                                return null;
                            }
                        }
                        //--------------------------
                        if (hasMaxCondition)
                        {
                            int max = -1;

                            if (!AdvancedConvertor.ToInt(maxValue.Trim(), ref max))
                            {
                                errString = "Invalid Maximum: " + max.ToString();
                                return null;
                            }

                            if (!(intVal <= max))
                            {
                                errString = invalidityError + ": " + value;
                                return null;
                            }
                        }

                        result = new SqlParameter(varNameInSqlProc, intVal);
                    }
                    break;
                //******************************************
                case ParamType.PT_BigInt:
                    {
                        if (paramValue == null)
                        {
                            result = new SqlParameter(varNameInSqlProc, System.Data.SqlDbType.BigInt);
                            result.Value = DBNull.Value;
                            break;
                        }

                        string value = paramValue.ToString().Trim();

                        Int64 bigintVal = -1;

                        if (!AdvancedConvertor.ToBigint(value, ref bigintVal))
                        {
                            errString = invalidityError + ": " + value;
                            return null;
                        }
                        //--------------------------
                        if (hasMinCondition)
                        {
                            Int64 min = -1;

                            if (!AdvancedConvertor.ToBigint(minValue.Trim(), ref min))
                            {
                                errString = "Invalid Minimum: " + min.ToString();
                                return null;
                            }

                            if (!(bigintVal >= min))
                            {
                                errString = invalidityError + ": " + value;
                                return null;
                            }
                        }
                        //--------------------------
                        if (hasMaxCondition)
                        {
                            Int64 max = -1;

                            if (!AdvancedConvertor.ToBigint(maxValue.Trim(), ref max))
                            {
                                errString = "Invalid Maximum: " + max.ToString();
                                return null;
                            }

                            if (!(bigintVal <= max))
                            {
                                errString = invalidityError + ": " + value;
                                return null;
                            }
                        }

                        result = new SqlParameter(varNameInSqlProc, bigintVal);
                    }
                    break;
                //******************************************
                case ParamType.PT_Decimal:
                    {
                        if (paramValue == null)
                        {
                            result = new SqlParameter(varNameInSqlProc, System.Data.SqlDbType.Decimal);
                            result.Value = DBNull.Value;
                            break;
                        }

                        string value = paramValue.ToString().Trim();

                        decimal decimalVal = -1;

                        if (!AdvancedConvertor.ToDecimal(value, ref decimalVal))
                        {
                            errString = invalidityError + ": " + value;
                            return null;
                        }
                        //--------------------------
                        if (hasMinCondition)
                        {
                            decimal min = -1;

                            if (!AdvancedConvertor.ToDecimal(minValue.Trim(), ref min))
                            {
                                errString = "Invalid Minimum: " + min.ToString();
                                return null;
                            }

                            if (!(decimalVal >= min))
                            {
                                errString = invalidityError + ": " + value;
                                return null;
                            }
                        }
                        //--------------------------
                        if (hasMaxCondition)
                        {
                            decimal max = -1;

                            if (!AdvancedConvertor.ToDecimal(maxValue.Trim(), ref max))
                            {
                                errString = "Invalid Maximum: " + max.ToString();
                                return null;
                            }

                            if (!(decimalVal <= max))
                            {
                                errString = invalidityError + ": " + value;
                                return null;
                            }
                        }

                        result = new SqlParameter(varNameInSqlProc, decimalVal);
                    }
                    break;
                //******************************************
                case ParamType.PT_String:
                    {
                        if (paramValue == null)
                        {
                            result = new SqlParameter(varNameInSqlProc, System.Data.SqlDbType.NVarChar);
                            result.Value = DBNull.Value;
                            break;
                        }

                        string value = paramValue.ToString();

                        result = new SqlParameter(varNameInSqlProc, value);
                    }
                    break;
                //******************************************
                case ParamType.PT_Image:
                    {
                        if (paramValue == null)
                        {
                            result = new SqlParameter(varNameInSqlProc, System.Data.SqlDbType.Image);
                            result.Value = DBNull.Value;
                            break;
                        }

                        Image value = (Image)paramValue;
                        result = new SqlParameter(varNameInSqlProc, SqlImageHelper.ImageToByteArray(value));
                    }
                    break;
                //******************************************
                case ParamType.PT_Bit:
                    {
                        if (paramValue == null)
                        {
                            result = new SqlParameter(varNameInSqlProc, System.Data.SqlDbType.Bit);
                            result.Value = DBNull.Value;
                            break;
                        }

                        string value = paramValue.ToString().Trim();

                        if (value.Length != 1 || (value != "0" && value != "1"))
                        {
                            errString = invalidityError + ": " + value;
                            return null;
                        }

                        result = new SqlParameter(varNameInSqlProc, value);
                    }
                    break;
                //******************************************
                case ParamType.PT_DateTime:
                    {
                        if (paramValue == null)
                        {
                            result = new SqlParameter(varNameInSqlProc, System.Data.SqlDbType.DateTime);
                            result.Value = DBNull.Value;
                            break;
                        }

                        result = new SqlParameter(varNameInSqlProc, paramValue);
                    }
                    break;
                //******************************************
                case ParamType.PT_Structured:
                    {
                        if (paramValue == null)
                        {
                            result = new SqlParameter(varNameInSqlProc, System.Data.SqlDbType.Structured);
                            result.Value = DBNull.Value;
                            break;
                        }

                        result = new SqlParameter(varNameInSqlProc, paramValue);
                    }
                    break;
                //******************************************
            }

            errString = "";
            return result;
        }
        //-------------------------------------------------------
    }
}
