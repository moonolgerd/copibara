Feature: Todo

  Scenario: Open Todo Page
    Given I have a todo list
    When I open the todo page
    Then I should see the todo list

  Scenario: Add Todo
    Given I have a todo list
    When I add a todo
      | Text             |
      | Something stupid |
      | Hell no          |
    Then I should see the todo in the list
      | Text             |
      | Something stupid |
      | Hell no          |

  Scenario: Remove Todo
    Given I have a todo list
    When I add a todo
      | Text             |
      | Something stupid |
      | Hell no          |
    And I remove the todo
      | Text    |
      | Hell no |
    Then I should see the todo in the list
      | Text             |
      | Something stupid |
