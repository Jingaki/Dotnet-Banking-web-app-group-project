using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DepositBankingWebApp.Data;
using DepositBankingWebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace DepositBankingWebApp.Controllers
{
    public class DepositsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DepositsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Deposits
        public async Task<IActionResult> Index()
        {
            /*
            var applicationDbContext = _context.Deposits.Include(d => d.ApplicationUser);
            return View(await applicationDbContext.ToListAsync());
            */
            var applicationDbContext = _context.Deposits.Include(d => d.ApplicationUser).ToList();
            return View(applicationDbContext);
        }
        // GET: Deposits/ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return _context.Deposits != null ?
                          View() :
                          Problem("Entity set 'ApplicationDbContext.Deposit'  is null.");
        }

        // GET: Deposits/ShowSearchResult
        public async Task<IActionResult> ShowSearchResult(bool SearchCheckStandartDeposit,
            CurrencyType SearchCurrencyType, InterestPaymentType SearchInterestPaymentType,
            OwnershipType SearchOwnershipType, bool SearchCheckTimeDeposit, bool SearchCheckOverdraft,
            bool SearchCheckCredit, float SearchSum)
        {
            return _context.Deposits != null ?
                          View("Index", await _context.Deposits
                          .Where(d => d.IsStandardTermDeposit.Equals(SearchCheckStandartDeposit))
                          .Where(d => d.CurrencyType.Equals(SearchCurrencyType))
                          .Where(d => d.InterestPaymentType.Equals(SearchInterestPaymentType))
                          .Where(d => d.OwnershipType.Equals(SearchOwnershipType))
                          .Where(d => d.TimeDeposit.Equals(SearchCheckTimeDeposit))
                          .Where(d => d.OverdraftPossability.Equals(SearchCheckOverdraft))
                          .Where(d => d.CreditPossability.Equals(SearchCheckCredit))
                          .Where(d => d.MinSum <= SearchSum)

                          //still needs to be filled with the all data variant and checkboxes to be changed on whatever...
                          .ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Deposit'  is null.");
        }



        //000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000//


        // GET: Deposits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Deposits == null)
            {
                return NotFound();
            }

            var deposit = await _context.Deposits
                .Include(d => d.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deposit == null)
            {
                return NotFound();
            }

            return View(deposit);
        }

        // GET: Deposits/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            return View();
        }

        // POST: Deposits/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IsStandardTermDeposit,IsInterestFixed,TimeDeposit,OverdraftPossability,CreditPossability,MonthlyCompounding,TerminalCapitalization,ValidForClientsOnly,CurrencyType,InterestPaymentType,OwnershipType,EffectiveAnnualInterestRate,WebLinkToOffer,DescriptionOfNegotiatedInterestRate,MinSum,MinSumDescription,MinDuration,MaxSum,MaxSumDescription,MaxDuration,DurationDescription,ApplicationUserId")] Deposit deposit)
        {
            if (ModelState.IsValid)
            //{ }else
            {
                _context.Deposits.Add(deposit); 
                await _context.SaveChangesAsync();
                //_context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", deposit.ApplicationUserId);
            return View(deposit);
        }

        // GET: Deposits/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Deposits == null)
            {
                return NotFound();
            }

            var deposit = await _context.Deposits.FindAsync(id);
            if (deposit == null)
            {
                return NotFound();
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", deposit.ApplicationUserId);
            return View(deposit);
        }

        // POST: Deposits/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IsStandardTermDeposit,IsInterestFixed,TimeDeposit,OverdraftPossability,CreditPossability,MonthlyCompounding,TerminalCapitalization,ValidForClientsOnly,CurrencyType,InterestPaymentType,OwnershipType,EffectiveAnnualInterestRate,WebLinkToOffer,DescriptionOfNegotiatedInterestRate,MinSum,MinSumDescription,MinDuration,MaxSum,MaxSumDescription,MaxDuration,DurationDescription,ApplicationUserId")] Deposit deposit)
        {
            if (id != deposit.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deposit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepositExists(deposit.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", deposit.ApplicationUserId);
            return View(deposit);
        }

        // GET: Deposits/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Deposits == null)
            {
                return NotFound();
            }

            var deposit = await _context.Deposits
                .Include(d => d.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deposit == null)
            {
                return NotFound();
            }

            return View(deposit);
        }

        // POST: Deposits/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Deposits == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Deposits'  is null.");
            }
            var deposit = await _context.Deposits.FindAsync(id);
            if (deposit != null)
            {
                _context.Deposits.Remove(deposit);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepositExists(int id)
        {
          return (_context.Deposits?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
