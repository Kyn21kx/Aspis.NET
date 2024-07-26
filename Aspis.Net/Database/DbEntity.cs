using System.ComponentModel.DataAnnotations;

namespace AspisNet.Database; 

/// <summary>
/// 
/// </summary>
/// <typeparam name="T">Type of the entity's Id</typeparam>
public class DbEntity<T> where T : struct {

	[Key]
	public T Id { get; set; }

}
