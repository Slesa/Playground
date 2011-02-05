using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Godot.IcsEditor.Ui.Localization;

namespace Godot.IcsEditor.Ui.Model
{
    public class EditSettings : IDataErrorInfo
    {
        public EditSettings()
        {
            //MatrixPath = Properties.Settings.Default.MatrixPath;
            //DbServer = Properties.Settings.Default.DbServer;
            //DbUser = Properties.Settings.Default.DbUser;
            //DbPassword = Properties.Settings.Default.DbPassword;
            //DbIntegrated = Properties.Settings.Default.DbIntegrated;
        }

        public void TakeOverSettings()
        {
            //Properties.Settings.Default.MatrixPath = MatrixPath;
            //Properties.Settings.Default.DbServer = DbServer;
            //Properties.Settings.Default.DbUser = DbUser;
            //Properties.Settings.Default.DbPassword = DbPassword;
            //Properties.Settings.Default.DbIntegrated = DbIntegrated;
            //Properties.Settings.Default.Save();
        }

        public void ResetSettings()
        {
            //Properties.Settings.Default.Reload();
        }

        public string MatrixPath { get; set;  }
        public string DbServer { get; set; }
        public bool DbIntegrated { get; set; }
        public string DbUser { get; set; }
        public string DbPassword { get; set; }

        #region IDataErrorInfo Members

        string IDataErrorInfo.this[string propertyName]
        {
            get { return GetValidationError(propertyName); }
        }

        string IDataErrorInfo.Error { get { return null; } }

        #endregion

        #region Validation

        public bool IsValid
        {
            get { return ValidatedProperties.All(property => GetValidationError(property) == null); }
        }

        static readonly string[] ValidatedProperties = 
        { 
            "MatrixPath",
            "DbServer",
            "DbUser",
        };

        string GetValidationError(string propertyName)
        {
            if (Array.IndexOf(ValidatedProperties, propertyName) < 0)
                return null;

            string error = null;
            switch (propertyName)
            {
                case "MatrixPath":
                    error = ValidateMatrixPath();
                    break;
                case "DbServer":
                    error = ValidateDbServer();
                    break;
                case "DbUser":
                    error = ValidateDbUser();
                    break;
            }
            return error;
        }

        string ValidateMatrixPath()
        {
            return !ValidateMatrixPath(MatrixPath) ? Strings.Model_EditSettings_MatrixPath_is_invalid : null;
        }

        public static bool ValidateMatrixPath(string path)
        {
            if (string.IsNullOrEmpty(path))
                path = Directory.GetCurrentDirectory();
            var directoryInfo = new DirectoryInfo(Path.Combine(path, @"usr\data"));
            return directoryInfo.Exists;
        }

        string ValidateDbServer()
        {
            return EditObject.IsStringMissing(DbServer) ? Strings.Model_EditSettings_DbServer_is_invalid : null;
        }

        string ValidateDbUser()
        {
            if (DbIntegrated)
                return null;
            return EditObject.IsStringMissing(DbUser) ? Strings.Model_EditSettings_DbUser_is_empty : null;
        }

        #endregion
    }
}

