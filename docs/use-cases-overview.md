# BabyDutyDuel - Use Cases Overview

## 📋 Alla Use Cases

### ✅ Dokumenterade med Aktivitetsdiagram (6 st)

| # | Use Case | Status | Implementerad | Diagram | Prioritet |
|---|----------|--------|---------------|---------|-----------|
| 1 | **Authentication Flow** | 📊 Documented | ✅ Yes | `activity-diagram-authentication.puml` | 🔴 Critical |
| 2 | **Search & Invite Partner** | 📊 Documented | ❌ No | `activity-diagram-search-invite-partner.puml` | 🟡 High |
| 3 | **Create Contract** | 📊 Documented | ✅ Yes | `activity-diagram-create-contract.puml` | 🔴 Critical |
| 4 | **Complete Chore** | 📊 Documented | ✅ Yes | `activity-diagram-complete-chore.puml` | 🔴 Critical |
| 5 | **Verify Completion** | 📊 Documented | ❌ No | `activity-diagram-verify-completion.puml` | 🟡 High |
| 6 | **System Overview** | 📊 Documented | ➖ N/A | `activity-diagram-system-overview.puml` | ℹ️ Info |

---

## 🚀 Implementerade Use Cases (8 st)

### Authentication (2)
- ✅ `LoginWithGoogleUseCase` - Login via Google OAuth
- ✅ `LoginWithAppleUseCase` - Login via Apple Sign In

### Contracts (2)
- ✅ `CreateContractUseCase` - Skapa kontrakt mellan föräldrar
- ✅ `GetContractByIdUseCase` - Hämta specifikt kontrakt

### BabyChores (3)
- ✅ `CreateBabyChoreUseCase` - Skapa ny bebissyssla
- ✅ `CompleteChoreUseCase` - Markera syssla som slutförd
- ✅ `GetChoresByContractUseCase` - Hämta alla sysslor för ett kontrakt

### Points (1)
- ✅ `GetLeaderboardUseCase` - Hämta leaderboard med poäng

---

## 🔨 Use Cases som behöver implementeras

### 🔴 Critical (Krävs för MVP)

| # | Use Case | Beskrivning | Komplexitet | Beror på |
|---|----------|-------------|-------------|----------|
| 1 | `AcceptContractInvitationUseCase` | Parent B accepterar kontrakt | Låg | Contract entity |
| 2 | `RefreshTokenUseCase` | Förnya JWT access token | Medium | Token service |
| 3 | `SearchParentByEmailUseCase` | Sök förälder via email | Låg | Parent repository |

### 🟡 High Priority (Viktigt för UX)

| # | Use Case | Beskrivning | Komplexitet | Beror på |
|---|----------|-------------|-------------|----------|
| 4 | `VerifyChoreCompletionUseCase` | Verifiera partners completion | Medium | ChoreCompletion entity |
| 5 | `GetActiveContractsUseCase` | Hämta användarens aktiva kontrakt | Låg | Contract repository |
| 6 | `SendInvitationUseCase` | Skicka email/notis invitation | Medium | Notification service |

### 🟢 Medium Priority (Nice to have)

| # | Use Case | Beskrivning | Komplexitet | Beror på |
|---|----------|-------------|-------------|----------|
| 7 | `GetBabyChoreByIdUseCase` | Hämta specifik chore | Låg | Chore repository |
| 8 | `UpdateBabyChoreUseCase` | Redigera chore details | Låg | Chore entity |
| 9 | `GetPointHistoryUseCase` | Visa poänghistorik över tid | Medium | Points aggregation |
| 10 | `UpdateParentProfileUseCase` | Uppdatera användarprofil | Låg | Parent entity |

### ⚪ Low Priority (Future features)

| # | Use Case | Beskrivning | Komplexitet | Beror på |
|---|----------|-------------|-------------|----------|
| 11 | `EndContractUseCase` | Avsluta kontrakt manuellt | Låg | Contract entity |
| 12 | `DeleteBabyChoreUseCase` | Ta bort chore | Låg | Chore repository |
| 13 | `CreateDisputeUseCase` | Skapa dispute för completion | Hög | Dispute entity |
| 14 | `ResolveDisputeUseCase` | Lösa dispute mellan föräldrar | Hög | Dispute workflow |
| 15 | `GenerateWeeklySummaryUseCase` | Skapa veckosammanfattning | Medium | Analytics |
| 16 | `SendWeeklySummaryUseCase` | Skicka veckorapport | Medium | Email service |
| 17 | `GetParentStatisticsUseCase` | Hämta användarstatistik | Medium | Analytics |
| 18 | `DeclineInvitationUseCase` | Neka kontrakt invitation | Låg | Invitation entity |
| 19 | `RevokeInvitationUseCase` | Återkalla skickad invitation | Låg | Invitation entity |

---

## 📊 Statistik

```
Total Use Cases Identified: 27
├── Implemented:            8  (30%)
├── Documented (diagram):   6  (22%)
├── Critical (not done):    3  (11%)
├── High Priority:          3  (11%)
├── Medium Priority:        4  (15%)
└── Low Priority:           9  (33%)
```

---

## 🎯 Implementation Roadmap

### Phase 1: MVP (Minimum Viable Product)
**Timeline: 2 veckor**

Week 1:
- [ ] `AcceptContractInvitationUseCase`
- [ ] `SearchParentByEmailUseCase`
- [ ] `RefreshTokenUseCase`

Week 2:
- [ ] `VerifyChoreCompletionUseCase`
- [ ] `GetActiveContractsUseCase`
- [ ] `SendInvitationUseCase`

**Result:** Fully functional app with core features

### Phase 2: Enhanced Features
**Timeline: 2 veckor**

- [ ] `GetBabyChoreByIdUseCase`
- [ ] `UpdateBabyChoreUseCase`
- [ ] `GetPointHistoryUseCase`
- [ ] `UpdateParentProfileUseCase`

**Result:** Better UX and user management

### Phase 3: Advanced Features
**Timeline: 3 veckor**

- [ ] Dispute system (Create, Resolve)
- [ ] Weekly summaries
- [ ] Analytics och statistics
- [ ] Invitation management

**Result:** Complete competitive platform

---

## 🏗️ Architecture Notes

### Use Case Structure
Alla use cases följer samma pattern:

```csharp
public class [Name]UseCase
{
    // Dependencies
    private readonly IRepository _repo;
    private readonly IUnitOfWork _unitOfWork;
    
    // Constructor injection
    public [Name]UseCase(IRepository repo, IUnitOfWork unitOfWork)
    {
        _repo = repo;
        _unitOfWork = unitOfWork;
    }
    
    // Execute method
    public async Task<ResultDto> ExecuteAsync(RequestDto request)
    {
        // 1. Validation
        // 2. Business Logic (Domain)
        // 3. Persistence
        // 4. Return DTO
    }
}
```

### Clean Architecture Layers

```
Use Case (Application) 
    ↓ uses
Repository Interface (Application)
    ↓ implemented by
Repository (Infrastructure)
    ↓ uses
Domain Entity (Domain)
```

---

## 📝 Notes

### Namespaces
- Authentication: `Application.UseCases.Authentication`
- Contracts: `Application.UseCases.Contracts`
- BabyChores: `Application.UseCases.BabyChores`
- Points: `Application.UseCases.Points`
- Parents: `Application.UseCases.Parents`
- Invitations: `Application.UseCases.Invitations`
- Disputes: `Application.UseCases.Disputes`

### Testing Strategy
Varje use case bör ha:
- ✅ Unit tests (mocked dependencies)
- ✅ Integration tests (real database)
- ✅ Activity diagram documentation

### Dependencies
Vissa use cases beror på andra:
- `VerifyChoreCompletionUseCase` → kräver `CompleteChoreUseCase`
- `GetLeaderboardUseCase` → kräver `CompleteChoreUseCase`
- `AcceptContractInvitationUseCase` → kräver `SendInvitationUseCase`

---

## 🔄 Latest Update

**Date:** 2024-03-XX
**Status:** 8 use cases implemented, 6 documented with diagrams
**Next:** Implement Phase 1 MVP use cases
