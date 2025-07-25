# Giới thiệu & Đặt vấn đề
## Tại sao cần Clean Architecture?
### Tránh spaghetti code
    Khi project càng lớn, việc trộn lẫn logic nghiệp vụ, truy vấn database và xử lý giao diện trong cùng 1 class khiến code trở nên rối rắm, khó đọc, khó debug – hay còn gọi là spaghetti code.

### Dễ bảo trì và mở rộng

Clean Architecture tách riêng các phần trong hệ thống. Nhờ đó:
    • Khi thêm tính năng mới hoặc sửa lỗi, chỉ cần chạm vào đúng tầng liên quan.
    • Giảm rủi ro “sửa chỗ này, chết chỗ kia”.

### Tăng khả năng kiểm thử (Testability)
    • Logic nghiệp vụ (Use Cases) không phụ thuộc vào Database hay UI → có thể test độc lập.
    • Viết unit test dễ dàng, ít phải mock, tốc độ nhanh.

### Giảm phụ thuộc vào công nghệ và framework
    • Logic cốt lõi nằm ở “vùng sạch”, không phụ thuộc Entity Framework, ASP.NET hay bất kỳ công nghệ cụ thể nào.
    • Muốn đổi framework, UI, hay database cũng dễ dàng hơn nhiều.

### Tăng độ bền vững cho dự án
    • Khi team thay người, khi công nghệ thay đổi, business logic vẫn được bảo toàn.
    • Hệ thống dễ “sống lâu – khỏe mạnh – ít drama”.

### Giúp làm việc nhóm hiệu quả hơn
    • Mỗi team hoặc dev đảm nhận phần việc rõ ràng: người lo UI, người lo logic, người lo database.
    • Dễ chia task, dễ review code, ít đụng nhau.

## Vấn đề của “monolith spaghetti code”
### Không có ranh giới rõ ràng giữa các phần
    • Giao diện (UI), xử lý nghiệp vụ (logic), và truy cập dữ liệu (data access) nằm lẫn lộn trong cùng một lớp hoặc cùng một project.
    • Khi sửa lỗi hoặc thêm tính năng, rất khó xác định đúng vị trí cần chỉnh sửa. Việc sửa một chỗ có thể gây lỗi ở chỗ khác.

### Khó bảo trì và mở rộng
    • Khi hệ thống phát triển lớn hơn, việc tìm hiểu, hiểu và thay đổi code trở nên phức tạp.
    • Việc thêm tính năng mới có thể mất nhiều thời gian do lo ngại ảnh hưởng đến các phần liên quan.

### Code phụ thuộc lẫn nhau chằng chịt
    • Các thành phần trong hệ thống gọi lẫn nhau, dẫn đến sự phụ thuộc vòng lặp (circular dependency).
    • Điều này khiến việc tái sử dụng hoặc tách biệt các module là rất khó, gần như không thể.

### Không thể kiểm thử độc lập
    • Logic nghiệp vụ phụ thuộc trực tiếp vào tầng dữ liệu hoặc UI, dẫn đến khó viết unit test.
    • Việc kiểm thử thường phải thực hiện toàn hệ thống, không tách rời được từng phần nhỏ.

### Khó áp dụng DevOps hoặc CI/CD
    • Kiến trúc monolith không hỗ trợ việc triển khai riêng từng phần của hệ thống.
    • Mỗi lần cập nhật phải build và deploy toàn bộ hệ thống, tốn thời gian và dễ sinh lỗi.

### Khó mở rộng đội ngũ phát triển
    • Khi nhiều người cùng tham gia vào một codebase lớn, không có cấu trúc rõ ràng, dễ gây xung đột, khó quản lý.
    • Việc onboarding người mới rất mất thời gian vì code khó đọc, thiếu tổ chức.

# Khái niệm Clean Architecture
## Định nghĩa (Uncle Bob)
### Định nghĩa
Clean Architecture là một mô hình kiến trúc phần mềm được đề xuất bởi Robert C. Martin (hay còn gọi là Uncle Bob), với mục tiêu chính là tổ chức codebase sao cho rõ ràng, tách biệt, dễ hiểu, dễ kiểm thử và dễ mở rộng.
Nội dung cốt lõi của Clean Architecture bao gồm:
    • Tách riêng logic nghiệp vụ (business rules) ra khỏi các thành phần phụ thuộc bên ngoài như cơ sở dữ liệu, giao diện người dùng, framework, v.v.
    • Thiết lập nguyên tắc phụ thuộc một chiều: các tầng bên ngoài có thể phụ thuộc vào tầng bên trong, nhưng không được làm ngược lại.

### Mục tiêu chính
    • Tăng tính độc lập của logic nghiệp vụ đối với framework, UI, hay database.
    • Tăng khả năng tái sử dụng và kiểm thử các phần cốt lõi.
    • Hỗ trợ bảo trì và phát triển lâu dài cho hệ thống.
    • Cho phép các thành phần thay đổi mà không ảnh hưởng đến toàn bộ hệ thống (ví dụ: thay đổi database hoặc giao diện mà không ảnh hưởng đến logic nghiệp vụ).

### Câu nói nổi tiếng của Uncle Bob:
"The architecture should tell us about the system, not about the frameworks it uses."
Tạm dịch: “Kiến trúc phần mềm nên phản ánh bản chất của hệ thống, chứ không phải framework mà nó sử dụng.”

## Mục tiêu cốt lõi
### Tách biệt rõ ràng giữa các tầng trong hệ thống
    • Mỗi tầng có một vai trò riêng: từ business logic, xử lý luồng nghiệp vụ đến tương tác với hệ thống bên ngoài.
    • Giúp hệ thống dễ hiểu, dễ quản lý và dễ mở rộng.

### Bảo vệ logic nghiệp vụ khỏi ảnh hưởng của công nghệ bên ngoài
    • Logic nghiệp vụ không phụ thuộc vào framework, database, hay UI.
    • Có thể thay đổi công nghệ (ví dụ: chuyển từ SQL sang NoSQL) mà không ảnh hưởng đến lõi hệ thống.

### Dễ dàng kiểm thử (testability)
    • Vì mỗi tầng độc lập nên có thể kiểm thử từng phần mà không cần setup toàn hệ thống.
    • Unit test cho logic nghiệp vụ trở nên đơn giản, nhanh và chính xác.

### Hỗ trợ bảo trì và phát triển dài hạn
    • Khi hệ thống phát triển, kiến trúc rõ ràng giúp tránh "nợ kỹ thuật".
    • Thay đổi yêu cầu nghiệp vụ không làm ảnh hưởng đến tầng bên ngoài.

### Tăng tính linh hoạt và khả năng tái sử dụng
    • Các thành phần có thể dễ dàng tái sử dụng trong các ứng dụng khác.
    • Việc chia nhỏ hệ thống thành các module độc lập giúp tăng khả năng mở rộng theo chiều ngang.

# Các tầng (Layers) chính
## Entities (Domain Models)
### Định nghĩa
    Entities là tầng sâu nhất và quan trọng nhất trong Clean Architecture.
    Nó chứa các đối tượng cốt lõi mô tả nghiệp vụ (business objects) và quy tắc nghiệp vụ cấp cao (enterprise rules).

### Đặc điểm chính
    • Độc lập hoàn toàn với bất kỳ công nghệ nào như framework, database, UI.
    • Không phụ thuộc vào bất kỳ tầng nào khác trong hệ thống.
    • Là nơi giữ logic nghiệp vụ bền vững, có thể tồn tại trong nhiều năm dù các công nghệ khác có thay đổi.

### Ví dụ
Giả sử đang xây dựng hệ thống quản lý đơn hàng, thì Order, Customer, Product có thể là các entity.
Mỗi entity có thể chứa:
    • Thuộc tính: 
    ```csharp OrderId, CustomerId, TotalAmount,… ```
    • Quy tắc nghiệp vụ: ví dụ 
    ```csharp Order.CalculateTotal(), Order.CanBeCancelled(). ```
```csharp
public class Order
{
    public Guid Id { get; private set; }
    public List<OrderItem> Items { get; private set; }
    public decimal TotalAmount => Items.Sum(i => i.Price * i.Quantity);

    public bool CanBeCancelled(DateTime now)
    {
        return (now - CreatedAt).TotalHours < 24;
    }

    public DateTime CreatedAt { get; private set; }
}
```

### Vai trò trong hệ thống
    • Là nền tảng ổn định nhất, nơi các tầng khác đều phụ thuộc vào.
    • Giúp đảm bảo hệ thống phản ánh đúng các quy tắc nghiệp vụ, chứ không chỉ là xử lý dữ liệu.

## Use Cases / Application
### Định nghĩa
Use Cases (hay còn gọi là Application Layer) là tầng chịu trách nhiệm triển khai các quy trình nghiệp vụ cụ thể của hệ thống.
Nó sử dụng các Entities để thực hiện nghiệp vụ, nhưng không biết gì về UI, database, hay framework bên ngoài.

### Chức năng chính
    • Thực thi các hành động cụ thể như: “Tạo đơn hàng”, “Xử lý thanh toán”, “Gửi email xác nhận”...
    • Đóng vai trò "điều phối viên" giữa các tầng, gọi repository, kiểm tra điều kiện nghiệp vụ, trả kết quả cho tầng ngoài (Controller/API).

### Đặc điểm
    • Phụ thuộc vào Entities, nhưng không phụ thuộc vào tầng UI hoặc tầng dữ liệu.
    • Chứa logic nghiệp vụ ở cấp ứng dụng, chứ không phải logic nghiệp vụ cốt lõi như Entities.
    • Các hành động thường được viết thành service, command handler, hoặc interactor.

### Ví dụ
```csharp
public class CreateOrderUseCase
{
    private readonly IOrderRepository _orderRepository;

    public CreateOrderUseCase(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Guid> ExecuteAsync(CreateOrderRequest request)
    {
        var order = new Order(request.CustomerId);
        foreach (var item in request.Items)
        {
            order.AddItem(item.ProductId, item.Quantity, item.Price);
        }

        await _orderRepository.AddAsync(order);
        return order.Id;
    }
}
```

### Vai trò trong hệ thống
    • Là cầu nối giữa logic nghiệp vụ (Entities) và thế giới bên ngoài (UI, DB, API).
    • Giúp hệ thống có thể thay đổi UI hay database mà không ảnh hưởng đến logic xử lý nghiệp vụ.

## Interface Adapters (Controllers, Gateways, Presenters)
### Định nghĩa
Interface Adapters là tầng chuyển đổi dữ liệu và điều phối giao tiếp giữa hệ thống bên trong và bên ngoài.
Nó đóng vai trò “trung gian dịch thuật” giữa các Use Case và công nghệ như Web API, cơ sở dữ liệu, hoặc UI.
### Chức năng chính
    • Nhận dữ liệu từ UI/API, chuyển thành dạng mà Use Case có thể xử lý (Input DTO).
    • Gọi các Use Case để xử lý logic nghiệp vụ.
    • Chuyển kết quả từ Use Case thành dữ liệu phù hợp để trả ra ngoài (ViewModel, JSON…).
    • Giao tiếp với database qua interface repository (thường là implementation của IGateway hoặc IRepository).
### Thành phần thường có
    • Controllers: nhận request từ phía người dùng (API, Web).
    • Gateways (Repositories): triển khai giao tiếp với cơ sở dữ liệu hoặc service bên ngoài.
    • Presenters / View Models: chuẩn bị dữ liệu để trả về UI hoặc API response.
### Ví dụ
1. Controllers
```csharp
[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly CreateOrderUseCase _createOrderUseCase;

    public OrdersController(CreateOrderUseCase createOrderUseCase)
    {
        _createOrderUseCase = createOrderUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderRequest request)
    {
        var orderId = await _createOrderUseCase.ExecuteAsync(request);
        return Ok(new { Id = orderId });
    }
}
```

2. Repository (Gateway Implementation)
```csharp
public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public async Task AddAsync(Order order)
    {
        var entity = OrderMapper.ToEntity(order);
        _context.Orders.Add(entity);
        await _context.SaveChangesAsync();
    }
}
```

### Vai trò trong hệ thống
Đảm bảo tầng trong (Application, Entities) không bị phụ thuộc vào các công nghệ bên ngoài như ASP.NET, EF Core, hay các API cụ thể.
Dễ dàng thay đổi giao diện (UI, Web API) hay thay đổi hạ tầng (database) mà không ảnh hưởng đến logic cốt lõi.

## Frameworks & Drivers (UI, DB, External APIs)
### Định nghĩa
Frameworks & Drivers là tầng ngoài cùng trong Clean Architecture, nơi chứa các thành phần cụ thể phụ thuộc vào công nghệ, chẳng hạn như:
Giao diện người dùng (Web UI, Blazor, Razor Pages…)
Framework backend (ASP.NET Core)
Database (SQL Server, MongoDB, PostgreSQL…)
Các dịch vụ bên ngoài (REST API, Message Queue, Email, File Storage, v.v.)

### Đặc điểm chính
Đây là nơi "đi vào và đi ra" của hệ thống, chịu trách nhiệm giao tiếp với thế giới bên ngoài.
Phụ thuộc vào các tầng bên trong, không được để tầng bên trong phụ thuộc lại.
Thường xuyên thay đổi theo yêu cầu công nghệ, nhưng logic cốt lõi không bị ảnh hưởng.

### Chức năng chính
Giao tiếp và triển khai cụ thể các cổng (gateways) được định nghĩa bởi tầng Application (Use Case).
Ví dụ:
ASP.NET Controller nằm ở đây, nhưng phụ thuộc vào interface ở Application.
Entity Framework DbContext nằm ở đây, triển khai repository interface của tầng trên.
Giao diện frontend (React, Angular) có thể gọi vào tầng Web API ở tầng này.
### Ví dụ
```csharp
public class SqlOrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public async Task<Order> GetByIdAsync(Guid id)
    {
        var entity = await _context.Orders.FindAsync(id);
        return OrderMapper.ToDomain(entity);
    }
}
```

### Vai trò trong hệ thống
Là nơi cắm công nghệ vào hệ thống, nhưng theo cách có kiểm soát.
Cho phép thay thế dễ dàng: từ EF → Dapper, hoặc từ REST → gRPC mà không động đến business logic.

# Nguyên tắc phụ thuộc (Dependency Rule)
## Luồng phụ thuộc từ “ngoài vào trong”
### Định nghĩa
Dependency Rule là nguyên tắc cốt lõi trong Clean Architecture, quy định rằng các tầng bên ngoài có thể phụ thuộc vào tầng bên trong, nhưng tầng bên trong tuyệt đối không được phụ thuộc ngược lại.

### Luồng phụ thuộc
Luồng phụ thuộc trong Clean Architecture luôn đi theo hướng:
Frameworks & Drivers → Interface Adapters → Use Cases → Entities
Nghĩa là:
    Use Cases có thể gọi Entities.
    Interface Adapters có thể gọi Use Cases.
    Frameworks có thể gọi Controller, Repository, Service ở tầng Adapter.
    Nhưng không bao giờ ngược lại.

### Ý nghĩa
Entities và Use Cases không được biết gì về web, database, hay giao diện người dùng.
Giúp hệ thống tách biệt hoàn toàn logic nghiệp vụ với công nghệ cụ thể.
Khi cần thay đổi công nghệ (ví dụ: từ ASP.NET MVC sang Blazor), bạn chỉ cần sửa các lớp ở tầng ngoài mà không ảnh hưởng đến logic bên trong.

### Ví dụ minh hoạ
Giả sử có class OrderController ở tầng Web (Frameworks), gọi vào CreateOrderUseCase ở tầng Application:

```csharp
var orderId = await _createOrderUseCase.ExecuteAsync(request);
```

Ngược lại, bạn không được viết logic trong Use Case kiểu như:

```csharp
var result = _dbContext.Orders.ToList();
```

### Giải pháp để giữ đúng nguyên tắc
Dùng interface (abstraction) để định nghĩa hành vi cần thiết (ví dụ: IOrderRepository).
Các tầng trong chỉ gọi qua interface.
Tầng ngoài chịu trách nhiệm triển khai interface (ví dụ: OrderRepository : IOrderRepository).

## Tầng trong không biết gì về tầng ngoài
Tới luôn phần này – **“Tầng trong không biết gì về tầng ngoài”** – là một trong những điểm mấu chốt làm nên sự “sạch sẽ” của Clean Architecture.
Dưới đây là nội dung rõ ràng – chặt chẽ – dễ hiểu để bạn đưa vào Word hoặc README:

---

## Tầng trong không biết gì về tầng ngoài

### Nguyên tắc cốt lõi

Trong Clean Architecture, tầng trong (ví dụ: `Entities`, `Use Cases`) không được phép biết bất kỳ điều gì về tầng ngoài như:

 Giao diện người dùng (UI)
 Cơ sở dữ liệu (DB)
 Framework (ASP.NET, Entity Framework, Blazor,…)
 API hoặc dịch vụ bên ngoài (Email, Message Queue,…)

Tầng trong chỉ biết đến những abstraction (giao diện), chứ không quan tâm cách mà tầng ngoài thực thi cụ thể.

### Vì sao lại như vậy?

 Để bảo vệ logic nghiệp vụ – phần cốt lõi của hệ thống – khỏi những thay đổi liên quan đến công nghệ.
 Đảm bảo code ở tầng trong luôn dễ test, dễ đọc và dễ tái sử dụng.
 Tránh tình trạng code business logic bị "dính chặt" với framework hoặc database cụ thể.

### Cách thực hiện

Để tầng trong không phụ thuộc tầng ngoài, Clean Architecture thường dùng:

Dependency Inversion Principle (DIP) – Đảo ngược sự phụ thuộc.
Interface-based Programming – Các tầng trong định nghĩa interface (ví dụ: `IOrderRepository`), tầng ngoài implement nó.

> Tầng trong “ra yêu cầu”, tầng ngoài “đáp ứng”.


### Ví dụ minh hoạ

2. Tầng trong:

```csharp
public interface IOrderRepository
{
    Task AddAsync(Order order);
}
```

1. Tầng ngoài:

```csharp
public class SqlOrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public async Task AddAsync(Order order)
    {
        var entity = OrderMapper.ToEntity(order);
        _context.Orders.Add(entity);
        await _context.SaveChangesAsync();
    }
}
```

Ở đây:
`Use Case` chỉ gọi `IOrderRepository`, không biết gì về AppDbContext hay EF Core.
EF Core được sử dụng chỉ ở tầng ngoài (Frameworks & Drivers).

# Ưu & nhược điểm
## Ưu điểm: maintainable, testable, scalable…
## Nhược điểm: learning curve, boilerplate ban đầu
# So sánh với các kiến trúc khác
## MVC / Layered Architecture
## Onion Architecture
## Hexagonal (Ports & Adapters)
# Áp dụng trong .NET
## Cấu trúc thư mục & project solution
## Ví dụ interface, DTO, service, repository
## Thư viện & template nổi bật (Jason Taylor, Ardalis)
# Best Practices & Tips
## Organization: tên project, naming convention
## Cách test từng layer
## Dependency Injection trong ASP.NET Core
# Các ví dụ minh hoạ
## Code snippet minh họa flow (từ Controller → Use Case → Entity)
## Sơ đồ sequence hoặc class diagram
# Kết luận & Hướng triển khai
## Các bước “bật đèn xanh” cho dự án .NET mới
## Lộ trình tiếp theo: refactor, mở rộng, tích hợp AI
