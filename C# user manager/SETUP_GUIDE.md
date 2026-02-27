# Setup Guide - Professional User Database Manager v2.0

This guide will walk you through setting up the application from scratch.

## 📋 Prerequisites

Before you begin, ensure you have the following installed:

### Required Software

1. **Visual Studio 2017 or later**
   - Download from: https://visualstudio.microsoft.com/
   - Required workload: .NET desktop development
   - Community Edition is sufficient

2. **MySQL Server 5.7 or higher**
   - Download from: https://dev.mysql.com/downloads/mysql/
   - Recommended: MySQL 8.0+
   - Note your root password during installation

3. **.NET Framework 4.7.2 or higher**
   - Usually included with Windows 10/11
   - Download from: https://dotnet.microsoft.com/download/dotnet-framework

### Optional but Recommended

- **MySQL Workbench**: GUI tool for MySQL management
- **HeidiSQL** or **phpMyAdmin**: Alternative MySQL management tools

## 🗄️ Database Setup

### Step 1: Start MySQL Server

**Windows:**
```bash
# Start MySQL service
net start MySQL80

# Or use Services app (services.msc)
# Find "MySQL80" and click Start
```

**Verify MySQL is running:**
```bash
mysql --version
```

### Step 2: Create Database and Table

**Option A: Using MySQL Command Line**

1. Open Command Prompt or PowerShell
2. Login to MySQL:
   ```bash
   mysql -u root -p
   ```
3. Enter your MySQL root password
4. Run the following SQL commands:

```sql
-- Create the database
CREATE SCHEMA users;

-- Use the database
USE users;

-- Create the table
CREATE TABLE usertable (
  ID int NOT NULL PRIMARY KEY,
  NV float NOT NULL,
  username varchar(50),
  usersurname varchar(50),
  category varchar(50),
  mission varchar(50)
);

-- Insert sample data
INSERT INTO usertable (ID, NV, username, usersurname, category, mission) VALUES
  (1, 10.22, 'Yunus', 'Vurgun', 'New Member', 'Programmer'),
  (2, 7.21, 'Fred', 'FreeMan', 'New Member', 'Programmer'),
  (3, 5.77, 'Ted', 'Tedsons', 'New Member', 'Designer'),
  (4, 3.21, 'Someone', 'Someonesons', 'New Member', 'Programmer'),
  (5, 9.44, 'Mr. BlaBla', 'someSurname', 'New Member', 'Designer');

-- Verify data
SELECT * FROM usertable;
```

**Option B: Using MySQL Workbench**

1. Open MySQL Workbench
2. Connect to your local MySQL server
3. Click "Create a new schema" button
4. Name it "users" and click Apply
5. Open a new SQL tab
6. Copy and paste the CREATE TABLE and INSERT statements from above
7. Execute the script

**Option C: Using the provided db.txt file**

The `db.txt` file in the project root contains all necessary SQL commands. You can:
1. Open it in a text editor
2. Copy all contents
3. Paste into MySQL Workbench or command line
4. Execute

### Step 3: Verify Database Setup

```sql
-- Check if database exists
SHOW DATABASES LIKE 'users';

-- Check if table exists
USE users;
SHOW TABLES;

-- Check table structure
DESCRIBE usertable;

-- Check data
SELECT * FROM usertable;
```

Expected output: 5 rows of sample data

## 🔧 Application Configuration

### Step 1: Configure Database Connection

The application needs to know how to connect to your MySQL database.

**Method 1: Using App.config (Recommended)**

1. Open `WindowsFormsApp2/WindowsFormsApp2/App.config`
2. Locate the `<connectionStrings>` section
3. Update the connection string with your MySQL credentials:

```xml
<connectionStrings>
    <add name="UserDatabase" 
         connectionString="server=localhost;user=root;password=YOUR_PASSWORD_HERE;port=3306;database=users;" 
         providerName="MySql.Data.MySqlClient"/>
</connectionStrings>
```

Replace `YOUR_PASSWORD_HERE` with your actual MySQL root password.

**Method 2: Using DatabaseConfig.cs**

1. Open `WindowsFormsApp2/Data/DatabaseConfig.cs`
2. Modify the default connection string:

```csharp
public static string ConnectionString
{
    get
    {
        if (string.IsNullOrEmpty(_connectionString))
        {
            _connectionString = "server=localhost;user=root;password=YOUR_PASSWORD;port=3306;database=users;";
        }
        return _connectionString;
    }
}
```

### Step 2: Verify MySQL.Data Package

1. Open the solution in Visual Studio
2. Right-click on the project → Manage NuGet Packages
3. Check if `MySql.Data` is installed
4. If not, search for "MySql.Data" and install it (version 8.0 or higher)

## 🏗️ Building the Application

### Step 1: Open Solution

1. Navigate to `C# user manager/WindowsFormsApp2/`
2. Double-click `WindowsFormsApp2.sln` to open in Visual Studio

### Step 2: Restore NuGet Packages

Visual Studio should automatically restore packages. If not:
1. Right-click on Solution → Restore NuGet Packages
2. Wait for restoration to complete

### Step 3: Build Solution

1. Click **Build** → **Build Solution** (or press `Ctrl+Shift+B`)
2. Check the Output window for any errors
3. Ensure build succeeds with "Build succeeded" message

### Step 4: Run Application

1. Press **F5** to run in Debug mode
2. Or press **Ctrl+F5** to run without debugging
3. The application window should appear

## ✅ Testing the Setup

### Test 1: Application Launches

- ✅ Application window opens without errors
- ✅ All tabs are visible (USER INFORMATION, N.VALUE BY MISSION, CATEGORY DISTRIBUTION, STATISTICS)
- ✅ Data grid shows 5 sample users

### Test 2: Database Connection

- ✅ Data grid is populated with users
- ✅ Charts display data
- ✅ Statistics tab shows correct numbers

### Test 3: Add User

1. Fill in all fields:
   - ID: 6
   - Numeric Value: 8.5
   - User Name: Test
   - User Surname: User
   - Category: New Member
   - Mission: Tester
2. Click **ADD**
3. ✅ Success message appears
4. ✅ New user appears in grid

### Test 4: Search

1. Go to USER INFORMATION tab
2. Type "Test" in search box
3. ✅ Grid filters to show only matching users

### Test 5: Update User

1. Click on the "Test User" row
2. Change Numeric Value to 9.0
3. Click **UPDATE**
4. ✅ Success message appears
5. ✅ Grid shows updated value

### Test 6: Delete User

1. Click on the "Test User" row
2. Click **DELETE**
3. Confirm deletion
4. ✅ Success message appears
5. ✅ User removed from grid

### Test 7: Export

1. Click **Export to CSV**
2. Choose a location and save
3. ✅ File is created
4. ✅ Open file to verify data

## 🐛 Troubleshooting

### Issue: "Unable to connect to database"

**Possible Causes:**
1. MySQL server is not running
2. Incorrect credentials
3. Database doesn't exist
4. Firewall blocking connection

**Solutions:**
```bash
# Check if MySQL is running
net start MySQL80

# Test connection manually
mysql -u root -p -h localhost

# Verify database exists
mysql -u root -p -e "SHOW DATABASES LIKE 'users';"
```

### Issue: "Authentication failed"

**Solution:**
1. Verify your MySQL password
2. Update App.config with correct password
3. Try resetting MySQL root password:
   ```bash
   ALTER USER 'root'@'localhost' IDENTIFIED BY 'new_password';
   FLUSH PRIVILEGES;
   ```

### Issue: "Table 'users.usertable' doesn't exist"

**Solution:**
1. Run the CREATE TABLE script again
2. Verify you're using the correct database:
   ```sql
   USE users;
   SHOW TABLES;
   ```

### Issue: "Could not load file or assembly 'MySql.Data'"

**Solution:**
1. Install MySql.Data NuGet package:
   ```
   Install-Package MySql.Data -Version 8.0.33
   ```
2. Rebuild solution

### Issue: Charts not displaying

**Solution:**
1. Ensure data exists in database
2. Click REFRESH button
3. Check that mission and category fields are populated
4. Verify System.Windows.Forms.DataVisualization is referenced

### Issue: Application crashes on startup

**Solution:**
1. Check Output window for error details
2. Verify .NET Framework 4.7.2 is installed
3. Run Visual Studio as Administrator
4. Clean and rebuild solution

## 🔒 Security Recommendations

### For Development

1. **Use a dedicated MySQL user** (not root):
   ```sql
   CREATE USER 'userapp'@'localhost' IDENTIFIED BY 'strong_password';
   GRANT ALL PRIVILEGES ON users.* TO 'userapp'@'localhost';
   FLUSH PRIVILEGES;
   ```

2. **Update connection string**:
   ```xml
   connectionString="server=localhost;user=userapp;password=strong_password;port=3306;database=users;"
   ```

### For Production

1. **Never commit passwords** to version control
2. **Use environment variables** for sensitive data
3. **Enable SSL** for MySQL connections
4. **Implement user authentication** in the application
5. **Regular backups** of the database
6. **Keep MySQL updated** to latest stable version

## 📚 Next Steps

After successful setup:

1. **Read the User Guide** in README.md
2. **Explore the code** to understand the architecture
3. **Customize the application** for your needs
4. **Add new features** following the established patterns
5. **Report issues** or contribute improvements

## 🆘 Getting Help

If you encounter issues not covered here:

1. Check the **Troubleshooting** section in README.md
2. Review the **CHANGELOG.md** for known issues
3. Search for similar issues online
4. Contact the development team
5. Create an issue in the repository

## ✨ Success!

If all tests pass, congratulations! Your Professional User Database Manager is ready to use.

Enjoy managing your users with a modern, secure, and professional application! 🎉
