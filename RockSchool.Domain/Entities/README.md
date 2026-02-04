# RockSchool Domain Entities - DDD Implementation

## Overview
This directory contains rich domain entities following Domain-Driven Design (DDD) principles. These entities encapsulate business logic and maintain invariants through their methods.

## Design Principles Applied

### 1. Encapsulation
- All properties have **private setters** to prevent external modification
- State changes only through business methods
- Validates business rules within the entity

### 2. Factory Methods
- Static `Create()` methods for object instantiation
- Ensures entities are created in valid states
- Replaces public constructors

### 3. EF Core Compatibility
- Private parameterless constructors for EF Core
- Navigation properties for relationships
- Collections exposed as `IReadOnlyCollection<T>`

### 4. Business Logic Encapsulation
- Domain validation within entity methods
- State transition logic (e.g., attendance status changes)
- Business rule enforcement (e.g., payment amounts > 0)

## Entity Catalog

### Core Business Entities

#### Subscription
**Purpose:** Manages student subscriptions with pricing and payments  
**Key Methods:**
- `Create()` - Factory method
- `RecordPayment(amount)` - Process payment and update outstanding balance
- `Cancel()` - Cancel subscription
- `AddSchedule(schedule)` - Add schedule to subscription
- `ApplyDiscount(discount)` - Apply discount to final price

**Collections:** Schedules, Tenders

#### Student
**Purpose:** Represents students enrolled in the school  
**Key Methods:**
- `Create()` - Factory method
- `UpdateInfo()` - Update student information
- `SetWaitingStatus()` - Toggle waiting status

**Collections:** BandStudents (bands the student is part of)

#### Teacher
**Purpose:** Represents instructors with their disciplines and schedules  
**Key Methods:**
- `Create()` - Factory method
- `UpdateInfo()` - Update teacher information
- `SetActiveStatus()` - Activate/deactivate teacher
- `AddDiscipline()` - Assign discipline to teacher
- `RemoveDiscipline()` - Remove discipline assignment

**Collections:** Disciplines, WorkingPeriods, ScheduledWorkingPeriods, Bands

#### Attendance
**Purpose:** Tracks student attendance for scheduled lessons  
**Key Methods:**
- `Create()` - Factory method
- `MarkAsAttended()` - Mark as attended
- `MarkAsMissed()` - Mark as missed
- `Cancel()` - Cancel attendance
- `AddComment()` - Add comment to attendance record
- `UpdateSchedule()` - Change associated schedule

**Business Rules:** Status transition validation

#### Schedule
**Purpose:** Manages lesson schedules with time and room assignments  
**Key Methods:**
- `Create()` - Factory method with time validation
- `UpdateTime()` - Update start/end times with validation
- `ChangeRoom()` - Reassign room

**Validation:** Start time < End time, WeekDay 0-6

### Supporting Entities

#### Branch
**Purpose:** Physical locations/branches of the school  
**Key Methods:**
- `Create()` - Factory method
- `UpdateInfo()` - Update name and address

**Collections:** Rooms, Teachers

#### Discipline
**Purpose:** Musical instruments or subjects taught  
**Key Methods:**
- `Create()` - Factory method
- `UpdateName()` - Update discipline name

**Collections:** RoomDisciplines, TeacherDisciplines

#### Room
**Purpose:** Physical rooms where lessons take place  
**Key Methods:**
- `Create()` - Factory method
- `UpdateInfo()` - Update room information
- `AddDiscipline()` - Assign discipline to room
- `RemoveDiscipline()` - Remove discipline assignment

**Collections:** RoomDisciplines, Schedules

#### Band
**Purpose:** Student bands/groups with teacher leadership  
**Key Methods:**
- `Create()` - Factory method
- `UpdateName()` - Update band name
- `ChangeTeacher()` - Assign new teacher
- `SetStatus()` - Update band status
- `AddStudent()` - Add student to band with role
- `RemoveStudent()` - Remove student from band

**Collections:** BandStudents, Schedules

#### Tender
**Purpose:** Payment transactions for subscriptions  
**Key Methods:**
- `Create()` - Factory method with amount validation

**Business Rules:** Amount must be > 0

#### Note
**Purpose:** Administrative notes/tasks with status tracking  
**Key Methods:**
- `Create()` - Factory method
- `UpdateDescription()` - Update note content
- `UpdateCompleteDate()` - Set expected completion date
- `Complete()` - Mark as completed with actual date
- `Cancel()` - Cancel the note
- `Reopen()` - Reopen completed note
- `AddComment()` - Add comment

**Business Rules:** Cannot cancel completed notes

### Many-to-Many Join Entities

#### RoomDiscipline
**Purpose:** Links rooms to disciplines they support  
**Composite Key:** RoomId + DisciplineId

#### TeacherDiscipline
**Purpose:** Links teachers to disciplines they teach  
**Composite Key:** TeacherId + DisciplineId

#### BandStudent
**Purpose:** Links students to bands with their role  
**Key Methods:**
- `Create()` - Factory method
- `ChangeRole()` - Update student's role in band

**Properties:** BandRoleId (instrument/role)

### Schedule Management Entities

#### WorkingPeriod
**Purpose:** Recurring weekly availability for teachers  
**Key Methods:**
- `Create()` - Factory method with validation
- `UpdateTime()` - Update time range
- `ChangeRoom()` - Reassign room
- `ChangeWeekDay()` - Change day of week

**Validation:** WeekDay 0-6, StartTime < EndTime

#### ScheduledWorkingPeriod
**Purpose:** Specific date instances of working periods  
**Key Methods:**
- `Create()` - Factory method
- `UpdateSchedule()` - Update date range
- `ChangeRoom()` - Reassign room

**Validation:** StartDate < EndDate

#### WaitingSchedule
**Purpose:** Student requests for future scheduling  
**Key Methods:**
- `Create()` - Factory method
- `UpdateSchedule()` - Update time preferences
- `ChangeTeacher()` - Request different teacher

**Properties:** CreatedOn timestamp for waitlist order

### Access Control Entities

#### User
**Purpose:** System users with authentication  
**Key Methods:**
- `Create()` - Factory method
- `UpdatePassword()` - Change password hash
- `ChangeRole()` - Update user role
- `Activate()` / `Deactivate()` - Toggle active status

#### Role
**Purpose:** User roles/permissions  
**Key Methods:**
- `Create()` - Factory method
- `UpdateName()` - Update role name
- `Activate()` / `Deactivate()` - Toggle active status

#### Payment
**Purpose:** Generic payment tracking  
**Key Methods:**
- `Create()` - Factory method
- `UpdateAmount()` - Modify payment amount
- `UpdatePaymentDate()` - Change payment date

## Usage Patterns

### Creating Entities
```csharp
// Always use factory methods
var subscription = Subscription.Create(
    studentId: studentId,
    teacherId: teacherId,
    disciplineId: disciplineId,
    price: 1000m,
    finalPrice: 900m
);

// DO NOT use constructors
// var subscription = new Subscription(); // ❌ Will not work
```

### Modifying State
```csharp
// Use business methods, not property setters
subscription.RecordPayment(300m); // ✅
// subscription.AmountOutstanding = 600m; // ❌ Compile error - private setter

// State changes encapsulate business logic
attendance.MarkAsAttended(); // Updates Status and validates transition
```

### Validating Business Rules
```csharp
// Validation happens automatically
try 
{
    var schedule = Schedule.Create(
        subscriptionId: subId,
        weekDay: 7, // Invalid!
        startTime: new DateTime(...),
        endTime: new DateTime(...),
        roomId: roomId
    );
}
catch (ArgumentException ex)
{
    // "Week day must be between 0 and 6"
}
```

## Next Steps

### Phase 2: Update Data Layer
- Update EF entities in `RockSchool.Data.Entities` to reference domain entities
- Configure EF mappings for domain entities
- Remove business logic from data entities (keep only persistence concerns)

### Phase 3: Repository Interfaces
- Create repository interfaces in Domain layer
- Define aggregate boundaries
- Implement in Data layer

### Phase 4: Application Services
- Extract use cases from controllers
- Orchestrate domain entities
- Keep business logic in domain

### Phase 5: Audit Fields
- Implement `BaseAuditableEntity` with CreatedDate, CreatedUser, EditDate, EditUser
- Override `SaveChangesAsync` in DbContext
- Apply to all entities

## Benefits Achieved

✅ **Encapsulation:** Business rules cannot be bypassed  
✅ **Testability:** Domain logic can be unit tested in isolation  
✅ **Maintainability:** Business rules centralized in domain  
✅ **Type Safety:** Invalid states prevented at compile-time  
✅ **Self-Documenting:** Methods reveal business operations  
✅ **Rich Domain Model:** Entities are not anemic property bags  

## Migration Strategy

Current architecture uses anemic models in `RockSchool.BL.Models`. The migration path:

1. ✅ **Created domain entities** (this directory)
2. ⏳ **Update services** to use domain entities from `RockSchool.Domain`
3. ⏳ **Update repositories** to map between EF entities and domain entities
4. ⏳ **Deprecate BL models** once domain entities are integrated
5. ⏳ **Remove mapping helpers** that map to BL models

This allows gradual migration without breaking existing functionality.
