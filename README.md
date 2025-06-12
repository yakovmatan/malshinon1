# Malshinon

An intelligence system for managing reports, identifying suspects, and classifying people as suspects or potential agents.

---

## 🧠 Project Description

Malshinon is a console-based system written in C#, connecting to a MySQL database, managing an intelligence information flow. The user can report on people, and the system analyzes the data and classifies participants according to activity patterns.

---

## 📁 Project Structure

- **malshinon1.dal.Dal**  
  Data Access Layer. Responsible for retrieving, updating, and inserting records into the database.
  
- **malshinon1.manager.HelpManeger**  
  Helper layer for managing input and user-related logic.

- **malshinon1.manager.Manager**  
  Manages the overall system flow: report submission, user analysis, printing dangerous targets and potential agents.

---

## 🗃️ Database

The system uses a database named `malshinon`, which includes the following tables:

- `people`: holds information about all people in the system.
- `intelreports`: intelligence reports.
- `alerts`: alerts about dangerous targets.

---

## 🔍 Main Features

- **Add new users** (reporters or targets).
- **Submit reports** including text with the target's name.
- **Count reports and mentions** for each user.
- **Automatic classification** of dangerous targets or potential agents based on rules:
  - More than 3 reports in 15 minutes → **dangerous target**.
  - Over 10 reports or an average report length over 100 characters → **potential agent**.
- **Create alerts** for dangerous targets.
- **Display lists** of dangerous targets and potential agents to the user.

---

## 🚀 How to Run

1. **Requirements:**
   - .NET 6 or newer
   - MySQL Server
   - Connection to `localhost` on the `malshinon` database

2. **Database Setup:**  
   Create the tables `people`, `intelreports`, and `alerts` with fields as per the code.

3. **Run the system:**  
   Run the `Manager` class → function `StartUsing()`.

---

## ⚙️ Example Usage Flow

1. User inputs their name → if not found, the system creates it.
2. User inputs a report text → system identifies the target from it.
3. If the target does not exist → create it.
4. Update the report and mention counters.
5. The system checks whether:
   - The reporter is a **potential agent**.
   - The target is a **dangerous target**.
6. Retrieve a list of dangerous targets or potential agents per the user's choice.

---

## 🧪 Sample Report

```text
Input: John Smith is planning an attack tomorrow night.
→ Target identified: John Smith
→ Report created
→ Reporter’s report count updated
→ Target’s mention count updated
→ Checked if target is dangerous/potential agent
