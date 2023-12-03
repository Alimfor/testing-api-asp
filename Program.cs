using Dapper.FluentMap;
using Exam.Entities;
using Exam.Repositories;
using Exam.Services;
using Exam.Utils.Mapper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policyBuilder =>
        {
            policyBuilder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

FluentMapper.Initialize(config =>
{
    config.AddMap(new AnswerOptionMapper());
    config.AddMap(new AnswerResultMapper());
    config.AddMap(new EntityStoreMapper());
    config.AddMap(new PersonAnswerMapper());
    config.AddMap(new PersonMapper());
    config.AddMap(new QuestionMapper());
    config.AddMap(new TestSessionMapper());
});

builder.Services.AddTransient<IAnswerOptionRepository, AnswerOptionRepository>();
builder.Services.AddTransient<IAnswerResultRepository, AnswerResultRepository>();
builder.Services.AddTransient<IPersonRepository, PersonRepository>();
builder.Services.AddTransient<IPersonAnswerRepository, PersonAnswerRepository>();
builder.Services.AddTransient<IQuestionRepository, QuestionRepository>();
builder.Services.AddTransient<ITestSessionRepository, TestSessionRepository>();

builder.Services.AddTransient<IAnswerOptionService, AnswerOptionService>();
builder.Services.AddTransient<IAnswerResultService, AnswerResultService>();
builder.Services.AddTransient<IPersonService, PersonService>();
builder.Services.AddTransient<IPersonAnswerService, PersonAnswerService>();
builder.Services.AddTransient<IQuestionService, QuestionService>();
builder.Services.AddTransient<ITestSessionService, TestSessionService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors();

app.MapControllers();

app.Run();