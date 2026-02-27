# Quick Reference Guide

## 🚀 Quick Start

1. **Start MySQL**: `net start MySQL80`
2. **Run Application**: Open in Visual Studio and press F5
3. **Default Connection**: localhost:3306, user: root, password: pass, database: users

## ⌨️ Keyboard Shortcuts

| Action | Shortcut |
|--------|----------|
| Build Solution | `Ctrl+Shift+B` |
| Run Application | `F5` |
| Run without Debug | `Ctrl+F5` |
| Stop Application | `Shift+F5` |

## 🎯 Common Tasks

### Add a User
```
1. Fill all fields (ID, NV, Name, Surname, Category, Mission)
2. Click ADD button
3. Verify in grid
```

### Update a User
```
1. Click row in grid
2. Modify fields
3. Click UPDATE button
```

### Delete a User
```
1. Click row in grid (or enter ID)
2. Click DELETE button
3. Confirm
```

### Search Users
```
1. Go to USER INFORMATION tab
2. Type in search box
3. Results filter automatically
```

### Export Data
```
1. Go to USER INFORMATION tab
2. Click "Export to CSV"
3. Choose location and save
```

## 📊 Database Quick Commands

### Connect to MySQL
```bash
mysql -u root -p
```

### View All Users
```sql
USE users;
SELECT * FROM usertable;
```

### Count Users
```sql
SELECT COUNT(*) FROM usertable;
```

### Find User by ID
```sql
SELECT * FROM usertable WHERE ID = 1;
```

### Get Statistics
```sql
SELECT 
  COUNT(*) as Total,
  AVG(NV) as Average,
  MAX(NV) as Maximum,
  MIN(NV) as Minimum
FROM usertable;
```

### Backup Database
```bash
mysqldump -u root -p users > backup.sql
```

### Restore Database
```bash
mysql -u root -p users < backup.sql
```

## 🎨 UI Color Codes

| Element | Color | Hex Code |
|---------|-------|----------|
| Primary Header | Blue | `#2196F3` |
| Add Button | Green | `#4CAF50` |
| Update Button | Orange | `#FF9800` |
| Delete Button | Red | `#F44336` |
| Refresh Button | Blue | `#2196F3` |
| Clear Button | Gray | `#9E9E9E` |
| Export Button | Dark Green | `#2E7D32` |

## 🔧 Configuration Files

### App.config Location
```
WindowsFormsApp2/WindowsFormsApp2/App.config
```

### Connection String Format
```xml
server=HOST;user=USER;password=PASS;port=PORT;database=DB
```

### Default Values
- Host: `localhost`
- User: `root`
- Password: `pass`
- Port: `3306`
- Database: `users`

## 📁 Project Structure

```
C# user manager/
├── README.md                    # Main documentation
├── SETUP_GUIDE.md              # Detailed setup instructions
├── CHANGELOG.md                # Version history
├── QUICK_REFERENCE.md          # This file
├── db.txt                      # Database SQL script
└── WindowsFormsApp2/
    └── WindowsFormsApp2/
        ├── Models/
        │   └── User.cs         # User entity
        ├── Data/
        │   ├── DatabaseConfig.cs    # DB configuration
        │   └── UserRepository.cs    # Data access
        ├── Utilities/
        │   ├── InputValidator.cs    # Validation
        │   └── ExportHelper.cs      # CSV export
        ├── Form1.cs            # Main form logic
        ├── Form1.Designer.cs   # UI designer
        └── App.config          # Configuration
```

## 🐛 Quick Troubleshooting

| Problem | Quick Fix |
|---------|-----------|
| Can't connect to DB | Check MySQL is running: `net start MySQL80` |
| Wrong password | Update App.config with correct password |
| Table not found | Run db.txt SQL script |
| Charts empty | Click REFRESH button |
| Duplicate ID error | Use different ID or UPDATE instead |
| Build errors | Restore NuGet packages |

## 📝 Field Validation Rules

| Field | Type | Required | Max Length | Special Rules |
|-------|------|----------|------------|---------------|
| ID | Integer | Yes | - | Must be positive, unique |
| Numeric Value | Float | Yes | - | Any decimal number |
| User Name | String | Yes | 50 | Cannot be empty |
| User Surname | String | Yes | 50 | Cannot be empty |
| Category | String | Yes | 50 | Cannot be empty |
| Mission | String | Yes | 50 | Cannot be empty |

## 🔒 Security Checklist

- ✅ All queries use parameterized commands
- ✅ Input validation on all fields
- ✅ Error handling with try-catch blocks
- ✅ No hardcoded passwords in code
- ✅ Connection strings in config file
- ✅ Duplicate ID prevention
- ✅ User confirmation for delete operations

## 📊 Statistics Formulas

```sql
-- Total Users
SELECT COUNT(*) FROM usertable;

-- Average Numeric Value
SELECT AVG(NV) FROM usertable;

-- Maximum Numeric Value
SELECT MAX(NV) FROM usertable;

-- Minimum Numeric Value
SELECT MIN(NV) FROM usertable;

-- Users by Category
SELECT category, COUNT(*) 
FROM usertable 
GROUP BY category;

-- Users by Mission
SELECT mission, COUNT(*) 
FROM usertable 
GROUP BY mission;
```

## 🎯 Best Practices

### Adding Users
1. Always use unique IDs
2. Fill all required fields
3. Use meaningful names
4. Verify data before clicking ADD

### Updating Users
1. Click row to load data
2. Modify only necessary fields
3. Double-check changes
4. Click UPDATE (not ADD)

### Deleting Users
1. Confirm you have the right user
2. Consider backing up first
3. Deletion is permanent
4. No undo available

### Searching
1. Search is case-insensitive
2. Searches across all text fields
3. Clear search to see all users
4. Use specific terms for better results

## 🔄 Maintenance Tasks

### Daily
- Backup database
- Check application logs
- Verify data integrity

### Weekly
- Review user statistics
- Export data for analysis
- Clean up test data

### Monthly
- Update MySQL if needed
- Review and optimize queries
- Check for application updates

## 📞 Support Resources

- **README.md**: Comprehensive documentation
- **SETUP_GUIDE.md**: Detailed setup instructions
- **CHANGELOG.md**: Version history and updates
- **Code Comments**: Inline documentation in source files

## 💡 Tips & Tricks

1. **Bulk Operations**: Use MySQL Workbench for bulk imports
2. **Data Validation**: Application validates before saving
3. **Quick Edit**: Click any row to load into form
4. **Export Before Delete**: Always export before major deletions
5. **Search Shortcuts**: Use partial names for faster searching
6. **Refresh Often**: Click REFRESH after external DB changes
7. **Clear Form**: Use CLEAR button to reset all fields
8. **Statistics**: Check STATISTICS tab for quick insights

## 🎓 Learning Resources

### C# & Windows Forms
- Microsoft Docs: https://docs.microsoft.com/dotnet
- Windows Forms Tutorial: https://docs.microsoft.com/windows/forms

### MySQL
- MySQL Documentation: https://dev.mysql.com/doc/
- MySQL Tutorial: https://www.mysqltutorial.org/

### Design Patterns
- Repository Pattern
- Model-View Pattern
- Singleton Pattern

---

**Version**: 2.0  
**Last Updated**: February 27, 2026  
**Quick Help**: Press F1 in application (if implemented)
