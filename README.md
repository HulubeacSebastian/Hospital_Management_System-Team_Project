# 🏥 DevCore Hospital App

![.NET](https://img.shields.io/badge/.NET-8-512BD4?logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-512BD4?logo=csharp&logoColor=white)
![WinUI 3](https://img.shields.io/badge/WinUI%203-Windows_App_SDK-0078D4?logo=windows11&logoColor=white)
![Windows](https://img.shields.io/badge/Platform-Windows-00A4EF?logo=windows&logoColor=white)
![SQL Server](https://img.shields.io/badge/Database-SQL%20Server-CC2927?logo=microsoftsqlserver&logoColor=white)

A Windows desktop hospital staff app for **scheduling shifts**, **managing appointments**, and **coordinating workflows** across roles like **Doctor**, **Pharmacist**, and **Admin**.

## 👤 My contributions (Team Lead)

This is a **team project (6 people)**. As the **team leader**, I coordinated the team effort (planning, task split, integration) and implemented the **Admin** features for:

- 🧠 **Automated Fatigue Management & Shift Balancing**
  - ✅ Auto-audit weekly rosters (e.g., max hours/week + minimum rest time between shifts)
  - 🚫 Block publishing when violations are detected + highlight conflicts
  - 🧩 Auto-suggest alternative staff with matching role/specialty and lowest workload (while preventing double-booking)
- 🚑 **Automated ER Dispatch & Doctor Status Management**
  - 🎯 Auto-match ER requests to doctors by specialization + real-time availability
  - 🔒 Validate doctor is on an active ER shift and status is **AVAILABLE**
  - 🔁 Update doctor status (e.g., set to **IN_EXAMINATION**) on successful assignment; flag unmatched requests for admin review

## ✨ What this app does

- 🧭 **Role-based navigation**: role selection + dashboards.
- 🗓️ **Shift scheduling**: view schedules, create/update shifts, detect overlapping work windows.
- 🔁 **Shift swap requests**: request / accept / reject / cancel swaps + related notifications.
- 📅 **Appointments**: doctor upcoming appointments, appointment details, admin appointment view.
- 🚑 **ER dispatch**: track ER requests (pending/assigned/unmatched/completed).
- 🧠 **Fatigue audit**: audit shift load / fatigue rules (admin tooling).
- 💊 **Pharmacy scheduling**: pharmacy roster + vacation/schedule views.
- 🩺 **Medical evaluations**: store and view evaluation data (diagnosis/notes/medications).
- 🍕 **Hangouts**: create social events + join participants (used for engagement/bonus logic).

## 🧰 Technologies used

- 🧑‍💻 **Language**: C# (nullable enabled)
- 🪟 **App framework**: **WinUI 3** via **Windows App SDK**
- 🧩 **Target**: `.NET 8` (`net8.0-windows`)
- 🎨 **UI**: XAML (`NavigationView`, `Frame` navigation)
- 🧱 **Architecture**: MVVM-style `Views/` + `ViewModels/` + `Services/` + `Repositories/`
- 🗄️ **Database**: **Microsoft SQL Server** (SQL scripts included)
- 🔌 **DB driver**: `Microsoft.Data.SqlClient`
- 📦 **Packaging**: MSIX tooling enabled in project settings

## 🗂️ Project structure

- `DevCoreHospital/DevCoreHospital/`: WinUI app (XAML + C#)
  - `Views/`, `ViewModels/`, `Models/`, `Services/`, `Repositories/`, `Data/`
- `HospitalDatabase.sql`: creates `HospitalDatabase` + tables + seed data
- `TestData.sql`: additional test data for ER requests & staff/shifts

## 🚀 Getting started

### ✅ Prerequisites

- **Windows 10/11**
- **.NET 8 SDK**
- **Visual Studio 2022** with **Windows App SDK / WinUI** tooling (recommended)
- **SQL Server** (e.g. **SQL Server Express**) + SQL Server Management Studio (SSMS)

### 1) 🧱 Create the database

1. Open `HospitalDatabase.sql` in SSMS
2. Execute it (it will drop & recreate `HospitalDatabase`)
3. Optional: run `TestData.sql` for extra scenario data

### 2) 🔐 Configure the connection string

The app currently uses a **hard-coded** SQL Server connection string in `DevCoreHospital/DevCoreHospital/AppSettings.cs`.

Update this value to match your SQL Server instance:

- `DevCoreHospital.Configuration.AppSettings.ConnectionString`

### 3) ▶️ Run the app

- **Visual Studio**: open `DevCoreHospital/DevCoreHospital.slnx` (or the project) and run.
- **CLI** (from the project directory containing `DevCoreHospital.csproj`):

```bash
dotnet restore
dotnet build
dotnet run
```

## 🧾 Database schema (high level)

Key tables:

- `Staff`, `Shifts`
- `Appointments`
- `Medical_Evaluations` (+ join tables)
- `ShiftSwapRequests`, `Notifications`
- `ER_Requests`
- `Hangouts`, `Hangout_Participants`
- `High_Risk_Medicines`

## 📝 Notes / important info

- 🔧 **Connection string**: currently points to `PATRICKPC\\SQLEXPRESS` by default; you will almost certainly need to change it.
- 🪟 **Windows-only**: this is a WinUI 3 desktop app and won’t run on macOS/Linux.
- 🌱 **Seeding**: `HospitalDatabase.sql` includes seed data for staff, shifts, swap requests, notifications, ER requests, etc.

