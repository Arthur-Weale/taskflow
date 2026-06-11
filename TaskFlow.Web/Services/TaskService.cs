using System.Net.Http.Json;
using TaskFlow.Web.Models;

namespace TaskFlow.Web.Services;

public class TaskService
{
    private readonly HttpClient _http;

    public TaskService(HttpClient http) => _http = http;

    public async Task<List<TaskItem>> GetAllAsync()
    {
        return await _http.GetFromJsonAsync<List<TaskItem>>("api/tasks") ?? new();
    }

    public async Task<TaskItem?> GetByIdAsync(int id)
    {
        return await _http.GetFromJsonAsync<TaskItem>($"api/tasks/{id}");
    }

    public async Task CreateAsync(TaskItem task)
    {
        await _http.PostAsJsonAsync("api/tasks", task);
    }

    public async Task UpdateAsync(int id, TaskItem task)
    {
        await _http.PutAsJsonAsync($"api/tasks/{id}", task);
    }

    public async Task DeleteAsync(int id)
    {
        await _http.DeleteAsync($"api/tasks/{id}");
    }

    public async Task ToggleCompleteAsync(int id)
    {
        await _http.PatchAsync($"api/tasks/{id}/complete", null);
    }
}
