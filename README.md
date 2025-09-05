# BlogBuilder 🚀

A modern, full-stack blogging platform built with ASP.NET Core MVC, featuring AI-powered content creation, robust authentication, and a rich user experience.

[![.NET](https://img.shields.io/badge/.NET-6.0+-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)](https://www.microsoft.com/en-us/sql-server)
[![Entity Framework](https://img.shields.io/badge/Entity_Framework-512BD4?style=for-the-badge&logo=nuget&logoColor=white)](https://docs.microsoft.com/en-us/ef/)
[![JWT](https://img.shields.io/badge/JWT-000000?style=for-the-badge&logo=JSON%20web%20tokens&logoColor=white)](https://jwt.io/)


![Architecture Diagram](./wwwroot/img/Screenshot%20(334).png)

## 🌟 Features

### 🔐 **Authentication & Security**
- **JWT-based Authentication** with secure token validation
- **Role-based Authorization** protecting sensitive operations
- **Secure Password Handling** with proper encryption
- **Session Management** with persistent login state

### ✍️ **Rich Blog Management**
- **WYSIWYG Editor** with formatting capabilities
- **Image Upload Support** with optimized storage (10MB limit)
- **Category System** with 15+ predefined topics
- **Content Validation** ensuring data integrity
- **Draft & Publish Workflow**

![Architecture Diagram](./wwwroot/img/Screenshot%20(336).png)


### 🤖 **AI-Powered Features**
- **Blog Writing Assistant** for content creation
- **Automatic Content Summarization**
- **Smart Content Suggestions**
- **Enhanced User Experience** through AI integration

### 🎨 **User Experience**
- **Responsive Design** optimized for all devices
- **Light/Dark Mode Toggle** with persistent preferences
- **Real-time Search** with instant results
- **Pagination System** for optimal performance
- **Interactive UI Elements** with smooth animations

### 📊 **Advanced Functionality**
- **Comment System** with full CRUD operations
- **User Profiles** and author attribution
- **Category Filtering** for content discovery
- **Search Functionality** across all blog content
- **Personal Blog Dashboard**


![Architecture Diagram](./wwwroot/img/Screenshot%20(335).png)

## 🏗️ Architecture

### **Layered MVC Architecture**

```
BlogBuilder/
├── Controllers/           # API endpoints and request handling
├── BusinessLayer/         # Business logic and services
├── RepositoryLayer/       # Data access and repository pattern
├── Models/               # Entity models and DTOs
├── Views/                # Razor views and UI components
└── wwwroot/              # Static assets (CSS, JS, images)
```

### **Design Patterns Implemented**
- **Repository Pattern** for data abstraction
- **Dependency Injection** for loose coupling
- **DTO Pattern** for data transfer
- **Service Layer** for business logic separation
- **MVC Pattern** for presentation layer organization

## 🛠️ Technology Stack

| Category | Technologies |
|----------|-------------|
| **Backend** | ASP.NET Core 6.0+, C# |
| **Frontend** | HTML5, CSS3, JavaScript, Razor Pages |
| **Database** | SQL Server, Entity Framework Core |
| **Authentication** | JWT Bearer Tokens |
| **Architecture** | MVC, Repository Pattern, Dependency Injection |
| **Tools** | Visual Studio, SQL Server Management Studio |

## 📋 Prerequisites

- [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) or later
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (Local or Express)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)
- [Git](https://git-scm.com/)

## 🚀 Quick Start

### 1. **Clone the Repository**
```bash
git clone https://github.com/vedantzope9/BlogBuilder.git 
cd BlogBuilder
```

### 2. **Configure Database**
Update the connection string in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=BLOG_PROJECT;Trusted_Connection=true;"
  }
}
```

### 3. **Setup JWT Configuration**
Add JWT settings to `appsettings.json`:
```json
{
  "Jwt": {
    "Key": "your-super-secure-key-here",
    "Issuer": "BlogBuilder",
    "Audience": "BlogBuilder-Users"
  }
}
```

### 4. **Database Migration**
```bash
dotnet ef database update
```

### 5. **Install Dependencies**
```bash
dotnet restore
```

### 6. **Run the Application**
```bash
dotnet run
```

Navigate to `https://localhost:5001` to access the application.

## 📁 Project Structure

### **Controllers**
- **`UserController`** - Authentication, registration, user management
- **`BlogController`** - Blog CRUD operations, search, filtering
- **`CommentsController`** - Comment management system

### **Key Features Implementation**

#### **🔐 Security Features**
```csharp
[Authorize]
public class CommentsController : Controller
{
    // JWT-protected endpoints
}
```

#### **📝 Rich Blog Editor**
- Image upload with base64 encoding
- Category-based organization
- Content validation and sanitization
- Responsive design elements

#### **🔍 Search & Discovery**
```csharp
[HttpGet("/Blog/SearchBlogs/{query}")]
public async Task<IActionResult> SearchBlogs(string query)
{
    var blogs = await _blogServices.SearchBlogs(query);
    return Ok(blogs.Select(b => new {
        b.BLOGID,
        b.BLOG_NAME,
        b.TOPIC_NAME
    }));
}
```

## 🎯 API Endpoints

### **Authentication**
- `GET /User/LoginUser` - Display login form
- `POST /User/LoginUser` - Authenticate user
- `GET /User/RegisterUser` - Display registration form
- `POST /User/RegisterUser` - Register new user

### **Blog Management**
- `GET /Blog` - List all blogs with pagination
- `GET /Blog/GetBlogById/{id}` - Get specific blog
- `POST /Blog/CreateBlog` - Create new blog (Auth required)
- `PUT /Blog/UpdateBlog` - Update blog (Auth required)
- `DELETE /Blog/DeleteBlog/{id}` - Delete blog (Auth required)

### **Content Discovery**
- `GET /Blog/GetBlogsByCategory/{category}` - Filter by category
- `GET /Blog/SearchBlogs/{query}` - Search blogs
- `GET /Blog/GetBlogsByUserId/{userId}` - User's blogs

### **Comments**
- `POST /Comments/AddComment` - Add comment (Auth required)
- `PUT /Comments/UpdateComment` - Update comment (Auth required)
- `DELETE /Comments/DeleteComment/{id}` - Delete comment (Auth required)

## 🔧 Configuration

### **File Upload Settings**
```csharp
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 10 * 1024 * 1024; // 10MB
});
```

### **JWT Authentication**
```csharp
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            // Additional validation parameters...
        };
    });
```

## 📱 Responsive Design

The application features a fully responsive design with:
- **Flexible grid system**
- **Touch-friendly interfaces**
- **Optimized image loading**
- **Progressive enhancement**

## 🎨 UI/UX Features

### **Modern Interface Elements**
- Interactive blog cards with hover effects
- Real-time search suggestions
- Category-based filtering
- Pagination for better performance
- Loading states and error handling

### **Accessibility**
- Semantic HTML structure
- Keyboard navigation support
- Screen reader compatibility
- High contrast mode support

## 🔒 Security Measures

- **JWT Token Authentication** with expiration
- **Input Validation** preventing XSS attacks
- **SQL Injection Protection** via Entity Framework
- **File Upload Restrictions** (size and type)
- **Authorization Attributes** on sensitive endpoints

## 📈 Performance Optimizations

- **Lazy Loading** for blog content
- **Pagination** to reduce load times
- **Image Optimization** with size restrictions
- **Caching Strategies** for frequently accessed data
- **Database Indexing** for faster queries


## 🎯 Future Enhancements

- [ ] **Real-time Notifications** for comments and interactions
- [ ] **Social Media Integration** for sharing blogs
- [ ] **Advanced Analytics Dashboard** for blog performance
- [ ] **Multi-language Support** for global audience
- [ ] **Progressive Web App (PWA)** features
- [ ] **Advanced Search** with filters and sorting
- [ ] **Blog Templates** for quick creation
- [ ] **Email Newsletter** integration

---
