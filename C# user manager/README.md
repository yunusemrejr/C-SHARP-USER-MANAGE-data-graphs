# Professional User Database Manager v2.0

A modern, secure, and feature-rich Windows Forms application for managing user data with MySQL database integration. This application has been completely modernized with professional architecture, enhanced security, and improved user experience.

![Version](https://img.shields.io/badge/version-2.0-blue)
![.NET Framework](https://img.shields.io/badge/.NET%20Framework-4.7.2-purple)
![MySQL](https://img.shields.io/badge/MySQL-8.0+-orange)

## 🌟 Features

### Core Functionality
- ✅ **CRUD Operations**: Create, Read, Update, and Delete user records
- 🔍 **Real-time Search**: Search users by name, surname, category, or mission
- 📊 **Data Visualization**: Interactive charts showing numeric values by mission and category distribution
- 📈 **Statistics Dashboard**: View total users, average/max/min numeric values
- 💾 **Export to CSV**: Export all user data to CSV format for external analysis
- 🔄 **Auto-refresh**: Refresh data with a single click
- 🎯 **Click-to-Edit**: Click any row in the grid to populate form fields for editing

### Security & Quality
- 🔒 **SQL Injection Prevention**: All queries use parameterized commands
- ✔️ **Input Validation**: Comprehensive validation for all user inputs
- 🛡️ **Error Handling**: Robust exception handling with user-friendly messages
- 🏗️ **Clean Architecture**: Separation of concerns with Models, Data Access Layer, and Utilities

### User Experience
- 🎨 **Modern UI**: Clean, professional interface with Material Design color scheme
- 📱 **Responsive Layout**: Well-organized tabs for different views
- 🖱️ **Intuitive Controls**: Clear labels and helpful tooltips
- ⚡ **Fast Performance**: Optimized database queries and data binding

## 📋 Requirements

- **Operating System**: Windows 7 or later
- **.NET Framework**: 4.7.2 or higher
- **Database**: MySQL Server 5.7 or higher
- **RAM**: Minimum 2GB
- **Disk Space**: 50MB for application

## 🚀 Installation & Setup

### 1. Database Setup

First, create the MySQL database and table using the provided SQL script:

```sql
-- Create database
CREATE SCHEMA users;

-- Create table
CREATE TABLE `users`.`usertable` (
  ID int NOT NULL Primary Key,
  NV float not null,
  username varchar(50),
  usersurname varchar(50),
  category varchar(50),
  mission varchar(50)
);

-- Insert sample data
INSERT INTO `users`.`usertable` (`ID`, `NV`, `username`, `usersurname`, `category`, `mission`) 
VALUES 
  (1, 10.22, 'Yunus', 'Vurgun', 'New Member', 'Programmer'),
  (2, 7.21, 'Fred', 'FreeMan', 'New Member', 'Programmer'),
  (3, 5.77, 'Ted', 'Tedsons', 'New Member', 'Designer'),
  (4, 3.21, 'Someone', 'Someonesons', 'New Member', 'Programmer'),
  (5, 9.44, 'Mr. BlaBla', 'someSurname', 'New Member', 'Designer');
```

### 2. Configure Database Connection

The application uses the following default connection settings:
- **Host**: localhost
- **Port**: 3306
- **Username**: root
- **Password**: pass
- **Database**: users

To change these settings, modify the `DatabaseConfig.cs` file:

```csharp
// Located in: WindowsFormsApp2/Data/DatabaseConfig.cs
DatabaseConfig.SetConnectionString("localhost", "root", "your_password", "users", 3306);
```

Or update the `App.config` file:

```xml
<connectionStrings>
  <add name="UserDatabase" 
       connectionString="server=localhost;user=root;password=your_password;port=3306;database=users;" 
       providerName="MySql.Data.MySqlClient"/>
</connectionStrings>
```

### 3. Build and Run

1. Open the solution in Visual Studio 2017 or later
2. Restore NuGet packages (MySQL.Data should be automatically restored)
3. Build the solution (F6)
4. Run the application (F5)

## 📖 User Guide

### Adding a New User

1. Fill in all required fields:
   - **User ID**: Unique numeric identifier (must be positive)
   - **Numeric Value**: Any decimal number
   - **User Name**: First name (max 50 characters)
   - **User Surname**: Last name (max 50 characters)
   - **Category**: User category (e.g., "New Member", "Senior")
   - **Mission**: User role (e.g., "Programmer", "Designer")

2. Click the **ADD** button
3. The new user will appear in the data grid

### Updating an Existing User

1. Click on any row in the **USER INFORMATION** tab to load the data into the form
2. Modify the desired fields (ID cannot be changed)
3. Click the **UPDATE** button
4. Confirm the changes

### Deleting a User

1. Enter the User ID in the ID field, OR click on a row to auto-populate
2. Click the **DELETE** button
3. Confirm the deletion when prompted

### Searching for Users

1. Go to the **USER INFORMATION** tab
2. Type in the search box at the top
3. Results will filter in real-time as you type
4. Search works across: username, surname, category, and mission fields

### Viewing Charts

- **N.VALUE BY MISSION**: Bar/column chart showing numeric values grouped by mission
- **CATEGORY DISTRIBUTION**: Pie chart showing the distribution of users by category

### Viewing Statistics

Navigate to the **STATISTICS** tab to see:
- Total number of users
- Average numeric value
- Maximum numeric value
- Minimum numeric value

### Exporting Data

1. Go to the **USER INFORMATION** tab
2. Click the **Export to CSV** button
3. Choose a location and filename
4. The file will be saved with all current data

### Refreshing Data

Click the **REFRESH** button to reload all data from the database. This is useful if:
- Multiple users are accessing the database
- You want to ensure you have the latest data
- After performing bulk operations outside the application

### Clearing the Form

Click the **CLEAR** button to reset all input fields and start fresh.

## 🏗️ Architecture

### Project Structure

```
WindowsFormsApp2/
├── Models/
│   └── User.cs                 # User entity model
├── Data/
│   ├── DatabaseConfig.cs       # Database configuration
│   └── UserRepository.cs       # Data access layer
├── Utilities/
│   ├── InputValidator.cs       # Input validation helpers
│   └── ExportHelper.cs         # CSV export functionality
├── Form1.cs                    # Main form logic
├── Form1.Designer.cs           # UI designer code
└── App.config                  # Application configuration
```

### Design Patterns Used

- **Repository Pattern**: `UserRepository` handles all database operations
- **Model-View Pattern**: Separation between UI (Form1) and data (User model)
- **Singleton Pattern**: `DatabaseConfig` for centralized configuration
- **Helper/Utility Pattern**: Static helper classes for validation and export

## 🔧 Technical Details

### Dependencies

- **MySql.Data** (8.0+): MySQL connector for .NET
- **System.Windows.Forms.DataVisualization**: For charts
- **System.Configuration**: For app configuration

### Database Operations

All database operations use **parameterized queries** to prevent SQL injection:

```csharp
// Example: Safe parameterized query
string query = "INSERT INTO usertable (ID, NV, username, ...) VALUES (@ID, @NV, @Username, ...)";
cmd.Parameters.AddWithValue("@ID", user.ID);
cmd.Parameters.AddWithValue("@NV", user.NV);
// ... more parameters
```

### Input Validation

The application validates:
- ✅ Numeric fields (ID, NV) are valid numbers
- ✅ Required fields are not empty
- ✅ String lengths don't exceed database limits
- ✅ IDs are positive integers
- ✅ Duplicate IDs are prevented

## 🎨 UI Improvements

### Color Scheme (Material Design)

- **Primary Blue**: `#2196F3` - Headers and primary actions
- **Success Green**: `#4CAF50` - Add button
- **Warning Orange**: `#FF9800` - Update button
- **Danger Red**: `#F44336` - Delete button
- **Info Blue**: `#2196F3` - Refresh button
- **Neutral Gray**: `#9E9E9E` - Clear button

### Typography

- **Headers**: Segoe UI, 18pt Bold
- **Labels**: Segoe UI, 9pt Regular
- **Inputs**: Segoe UI, 10pt Regular
- **Buttons**: Segoe UI, 11pt Bold

## 🐛 Troubleshooting

### "Unable to connect to database"

**Solution**: 
1. Ensure MySQL server is running
2. Verify connection credentials in `DatabaseConfig.cs` or `App.config`
3. Check if the `users` database exists
4. Verify MySQL port (default: 3306) is not blocked by firewall

### "User with this ID already exists"

**Solution**: 
- Use a different ID for new users
- Use the UPDATE function instead of ADD to modify existing users
- Check the database for existing IDs

### "Authentication to host 'localhost' failed"

**Solution**:
1. Verify MySQL username and password
2. Ensure the MySQL user has proper permissions:
   ```sql
   GRANT ALL PRIVILEGES ON users.* TO 'root'@'localhost';
   FLUSH PRIVILEGES;
   ```

### Charts not displaying

**Solution**:
1. Ensure there is data in the database
2. Click the REFRESH button
3. Check that the `mission` and `category` fields are populated

## 📝 Version History

### Version 2.0 (Current)
- ✨ Complete modernization with clean architecture
- 🔒 Enhanced security with parameterized queries
- ✔️ Comprehensive input validation
- 🔍 Real-time search functionality
- 📊 New statistics dashboard
- 💾 CSV export feature
- 🎨 Modern Material Design UI
- 🔄 Update user functionality
- 🖱️ Click-to-edit from data grid
- 📈 Improved charts and visualizations

### Version 1.0 (Original)
- Basic CRUD operations
- Simple data grid view
- Basic charts
- MySQL connectivity

## 👨‍💻 Development

### Adding New Features

To add new features, follow the established architecture:

1. **Models**: Add new entity classes in `Models/`
2. **Data Access**: Extend `UserRepository` or create new repositories in `Data/`
3. **UI**: Add new forms or controls in the main form
4. **Validation**: Add validation methods in `Utilities/InputValidator.cs`

### Code Style

- Use meaningful variable names
- Add XML documentation comments for public methods
- Follow C# naming conventions (PascalCase for methods, camelCase for private fields)
- Keep methods focused and single-purpose
- Handle exceptions appropriately

## 📄 License

This project is provided as-is for educational and commercial use.

## 🤝 Contributing

Contributions are welcome! Please follow these guidelines:
1. Fork the repository
2. Create a feature branch
3. Make your changes with clear commit messages
4. Test thoroughly
5. Submit a pull request

## 📧 Support

For issues, questions, or suggestions:
- Create an issue in the repository
- Contact the development team
- Check the troubleshooting section above

## 🙏 Credits

**Original Version**: Yunus Emre Vurgun (2022)  
**Modernized Version 2.0**: Enhanced with professional architecture and features

---

**Made with ❤️ using C# and Windows Forms**
