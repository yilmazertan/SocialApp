using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerApp.Data;
using ServerApp.DTO;
using ServerApp.Model;

namespace ServerApp.Controllers
{
   
[ApiController]
[Route("api/[controller]")]    
public class ProductsController:ControllerBase
    {
       
       private readonly SocialContext _context;

       public ProductsController(SocialContext context)
       {
           _context=context;
       }

    
    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var products =await _context
        .Products
        .Select(p=> new ProductDTO(){
            id=p.id,
            name=p.name,
            price=p.price,
            isactive=p.isactive
        })
        .ToListAsync();

        return Ok(products);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(int id)
    {
        var product=await _context
         .Products
         .FindAsync(id);

        if (product==null){
            return NotFound();
        }

        return Ok(ProductToDTO(product));

    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(Product product)
    {
       
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return  CreatedAtAction(nameof(GetProduct), new{id = product.id}, ProductToDTO(product));
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, Product product)
    {
        if (id!=product.id)
        {
            return BadRequest();
        }

        var p = await _context.Products.FindAsync(id);

        if(p==null)
        return NotFound();
                
         p.name=product.name;
         p.price=product.price;

         try{
             await _context.SaveChangesAsync();
         }
         catch{
             return NotFound();
         }  

            return NoContent();

    }



 [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product=await _context.Products.FindAsync(id);
        if(product==null)
        {
            return NotFound();
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return NoContent();
    }


private static ProductDTO ProductToDTO(Product p)
{
    return new ProductDTO()
    {
        id=p.id,
        name=p.name,
        price=p.price,
        isactive=p.isactive

    };
}










    }


   
  
}