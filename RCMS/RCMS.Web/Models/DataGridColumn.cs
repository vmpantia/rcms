using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;

namespace RCMS.Web.Models;

public class DataGridColumn<TData>
{
    private DataGridColumn(string title, bool isTemplate, Expression<Func<TData, string>>? property, Type? propertyType, 
        Func<TData, RenderFragment>? templateContent, bool sortable, Func<TData, object>? sortBy)
    {
        Title = title;
        IsTemplate = isTemplate;
        Property = property;
        PropertyType = propertyType;
        TemplateContent = templateContent;
        Sortable = sortable;
        SortBy = sortBy;
    }
    
    public string Title { get; }
    public bool IsTemplate { get; }
    public Expression<Func<TData, string>>? Property { get; }
    public Type? PropertyType { get; }
    public Func<TData, RenderFragment>? TemplateContent { get; }
    public bool Sortable { get; }
    public Func<TData, object>? SortBy { get; }
    
    public static DataGridColumn<TData> CreatePropertyColumn(string title, Expression<Func<TData, string>> property, bool sortable = true)
    {
        return new DataGridColumn<TData>(title, false, property, typeof(string), null, sortable, property.Compile());
    }
    
    public static DataGridColumn<TData> CreateTemplateColumn(string title, Func<TData, RenderFragment> templateContent, Func<TData, object>? sortBy = null)
    {
        return new DataGridColumn<TData>(title, true, null, null, templateContent, sortBy is not null, sortBy);
    }
}