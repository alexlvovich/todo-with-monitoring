# Simple .NET Todo Application with Konso Monitoring

This is a simple .NET todo application that integrates with Konso, a SaaS monitoring platform, to capture logs, track values, and collect metrics. The application allows users to create and manage a list of tasks or todos.

## Features

- User can add tasks to the todo list.
- User can mark tasks as completed.
- User can delete tasks from the todo list.
- User can view the list of tasks.
- Application captures logs for debugging and monitoring purposes.
- Application tracks the value of specific variables for analysis.
- Application records metrics to measure performance and usage statistics.

## Installation

1. Clone the repository from GitHub:

   ```
   git clone https://github.com/alexlvovich/todo-with-monitoring
   ```

2. Open the solution file (`todo.sln`) in Visual Studio.

3. Build the solution to restore NuGet packages and compile the project.

4. Run the application.

## Usage

1. Open your preferred web browser and visit `http://localhost:xx` to access the todo application.

2. Use the provided interface to manage your todo list:
   - To add a new task, enter the task description and click the "Add" button.
   - To edit a task, click the edit link by the task.
   - To delete a task, click the "Delete" button next to the task.

## Monitoring

The todo application integrates with Konso, a monitoring platform that enables capturing logs, tracking values, and collecting metrics. Konso provides insights into the application's performance and usage statistics. The following monitoring capabilities are included:

### Logging

The application logs events and errors, which are sent to Konso for centralized logging. Logs are crucial for debugging and monitoring the application's behavior.

### Value Tracking

The application tracks the value of specific variables and sends them to Konso for analysis. This tracking helps in understanding the behavior and changes in critical variables over time.

### Metrics

The application collects various metrics and sends them to Konso for analysis and monitoring. Metrics provide insights into the application's performance, usage statistics, and trends. These metrics can be used to identify performance bottlenecks and make data-driven decisions.

To access the monitoring features provided by Konso, follow these steps:

1. Visit the Konso website at [https://app.konso.io](https://app.konso.io) and sign up to your account.

2. Create a new project in Konso to represent your todo application.

3. Retrieve the Konso API key for your project.

4. In the todo application's codebase, locate the configuration file (`appsettings.json` or similar) and update the Konso API key with your own.

   ```json
   "Konso": {
    "Logging": {
      "Endpoint": "https://apis.konso.io",
      "BucketId": "<your bucket>",
      "ApiKey": "<your app key>",
      "App": "todoapp",
      "Level": "Trace"
    },
    "ValueTracking": {
      "Endpoint": "https://apis.konso.io",
      "BucketId": "<your bucket>",
      "ApiKey": "<your app key>"
    },
    "Metrics": {
      "Endpoint": "https://apis.konso.io",
      "BucketId": "<your bucket>",
      "ApiKey": "<your app key>",
      "App": "todoapp"
    }
  }
   ```

5. Rebuild and run the todo application. Now, logs, tracked values, and metrics will be sent to Konso for monitoring and analysis.

For further details on how to explore the monitoring features provided by Konso, refer to the Konso documentation.

## Contributing

Contributions to this todo application are welcome. If you encounter any issues or have suggestions for improvements, please submit a pull request or open an issue on the project's GitHub repository.

## License

This todo application is licensed under the [MIT License](LICENSE). Feel free to use, modify, and distribute the code as per the terms of the license.

## Contact

If you have any questions or need further assistance, please contact [your email address].
