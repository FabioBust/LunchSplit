# LunchSplit

LunchSplit is a C# application developed for Software Quality I. The application handles restaurant bill splitting between multiple attendees while supporting different tip options, weighted splitting, attendee exclusion, validation, and multiple rounding methods.

The project was developed using Test-Driven Development (TDD) and MSTest.

## Features

- No tip, percentage tip, and fixed tip calculations
- Equal and weighted bill splitting
- Attendee inclusion and exclusion
- Banker's rounding
- Ceiling rounding
- Floor rounding
- No-rounding option
- Penny reconciliation for uneven splits
- Bill and attendee validation
- Receipt formatting
- Automated unit testing with MSTest

## Project Structure

The solution contains two projects:

- `LunchSplit` - Contains the application and business logic.
- `LunchSplit.Tests` - Contains the automated MSTest test suite.

The main application classes are:

- `Bill`
- `Attendee`
- `Share`
- `Splitter`
- `Rounder`
- `BillValidator`
- `ReceiptFormatter`

## Requirements

- .NET 10
- Visual Studio
- MSTest

## Build

To build the project in Visual Studio:

1. Open the LunchSplit solution.
2. Select **Build > Build Solution**.

The project can also be built from the solution directory using:

```bash
dotnet build
```

## Run

To run the LunchSplit application:

```bash
dotnet run --project LunchSplit
```

## Run Unit Tests

Tests can be executed using Visual Studio Test Explorer:

1. Select **Test > Test Explorer**.
2. Select **Run All Tests**.

Tests can also be executed from the solution directory using:

```bash
dotnet test
```

## Testing

The project uses MSTest and follows the Arrange-Act-Assert (AAA) structure.

The test suite contains 27 test methods covering typical, edge, and negative scenarios involving:

- Tip calculations
- Rounding
- Penny reconciliation
- Bill validation
- Equal and weighted bill splitting
- Excluded attendees
- Tax and tip calculations
- Receipt formatting

Two tests are intentionally kept in a **RED/PENDING** state as TDD placeholders for functionality planned for future development:

- CSV receipt exporting
- Stripe payment integration

These two tests are expected to fail until the corresponding features are implemented.

## Author

Fabio Bustamante Romillo
