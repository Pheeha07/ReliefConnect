# 🌍 ReliefConnect  

**Student:** Precious Pheeha  
**Student Number:** ST10388513  
**Module:** Applied Programming  

ReliefConnect is an ASP.NET Core MVC web application designed to support disaster management through **community collaboration**. It provides features for reporting incidents, managing relief projects, coordinating volunteers, and donating resources.  

---

## 🎨 Colour Scheme & UI
- **Primary Theme**: Shades of **Purple (#6f42c1)** for the navbar and buttons.  
- **Secondary Colours**: White and soft gray for backgrounds.  
- **Contrast**: Text-dark (black) links for readability, with hover effects.  
- **Bootstrap 5** is used for responsive design and mobile-first layouts.  

The interface is **clean, modern, and accessible**, following **WCAG 2.0 guidelines** (contrast, semantic HTML, keyboard navigation).  

---

## 🚀 Features

### 🔑 User Authentication
- Register and login securely with ASP.NET Identity.  
- Session-based authentication with role support (Admin, Volunteer, Donor).  
- Navigation bar updates based on login status:
  - Logged-in users see **“My Profile”** and **Logout**.  
  - Guests see **Register** and **Login**.  

---

### 📋 Profile Management
- Users can create and manage their **Profile** (Full Name, Email, Phone, Role).  
- Profile automatically links to user login credentials.  

---

### 🆘 Disaster Incident Reporting
- Users can report disaster incidents (title, description, location, severity).  
- Data is stored in the database for Admin review.  
- Admins can manage, view, and assess reports.  

---

### 🎁 Resource Donations *(Partially Implemented)*
- Donation model created with fields for:
  - Amount  
  - Payment Method  
  - Relief Project (optional)  
  - Linked User  
- **Donation Items** (description + quantity) are supported.  
- Database setup is ready, but full CRUD UI is work in progress.  

---

### 🤝 Volunteer Management
- Volunteers can browse **available tasks**.  
- Users can sign up for tasks via the **VolunteerAssignments** controller.  
- Tasks include details like title, description, capacity, and schedule.  
- Admins can assign/cancel volunteers as needed.  

---

### 🏗️ Relief Projects
- Relief projects can be created by admins.  
- Each project links to **volunteer tasks** and optionally **donations**.  
- Includes fields like Title, Description, Location, and DateCreated.  

---

## 🛠️ Tech Stack
- **Framework:** ASP.NET Core MVC (C#)  
- **Database:** SQL Server (Entity Framework Core)  
- **Authentication:** ASP.NET Identity  
- **UI Framework:** Bootstrap 5  
- **Version Control:** Git & GitHub  
