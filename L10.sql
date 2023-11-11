Select p.ProductName, p.UnitPrice, c.CategoryName
from products p
join Categories c on p.CategoryID = c.CategoryID
order by CategoryName, p.ProductName

select c.CustomerID, count(o.OrderID) as totalorders
from Customers c
left join Orders o on c.CustomerID = o.CustomerID
group by c.CustomerID
order by totalorders DESC

select e.EmployeeID, e.FirstName, et.TerritoryID, t.TerritoryDescription
from Employees e
join EmployeeTerritories et on e.EmployeeID = et.EmployeeID
join Territories t on et.TerritoryID = t.TerritoryID