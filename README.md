# bpmn

_This software was created as the solution to a university assignment and should (probably) not be used in a production environment._

---

A simple Camunda BPMN poc with external C#/.NET service integration and a JavaScript-based DMN sub-activity. The goal was to automate a (simulated) assignment evaluation/grading process. 

The simulated assignment evaluation process follows the following steps:

1. Student submits an "assignemnt" via Camunda Forms
2. A plagiarism check is simulated via an external C# worker service
    - A manual plagiarism check by the examiner is requested if more than 5% and less than 50% of the content appear to be plagiarized (or if an exception occurs in the worker service).
3. The assignment is graded in a JavaScript-based DMN sub-activity.
    - Grading is purely simulated on whether the content of the assignment contains the same keywords used in the title.
    - A grade is determined based on how many title keywords appear in the content of the assignment.
    - The grades used are the German univerity grades (1.0 (best) to 5.0 (worst)).
4. Either a "You Passed" or "You Failed" email is sent via the C# worker service to the student.

## Prerequisites

- [Camunda Platform 7](https://camunda.com/download/)
- [Camunda Modeler](https://camunda.com/download/modeler/)

## Installation

1. Download the .NET worker service from the [Releases](https://github.com/frederik-hoeft/bpmn/releases) tab.
2. Configure the `smtp-settings.json` file to contain a valid email to be used for sending "You Passed" or "You Failed" emails.
3. Camunda Platform 7 should be running locally on port 8080.
4. Start the .NET worker service (`CamundaCheckPlagiarism.exe`)
5. Deploy the Camunda process instance

## Building from source

- Visual Studio 2022 and .NET 6 are required.
